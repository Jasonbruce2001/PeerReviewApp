using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using PeerReviewApp.Data;
using PeerReviewApp.Models;

namespace PeerReviewApp.Controllers
{
    [Authorize(Roles = "Instructor,Admin")]
    public class CoursesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public CoursesController(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Courses
        public async Task<IActionResult> Index()
        {
            // If admin, show all courses
            if (User.IsInRole("Admin"))
            {
                return View(await _context.Courses.ToListAsync());
            }

            // If instructor, show only their courses
            var user = await _userManager.GetUserAsync(User);
            var courses = await _context.Courses
                .Where(c => c.Instructor.Id == user.Id)
                .ToListAsync();

            return View(courses);
        }

        // GET: Courses/StudentCourses
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> StudentCourses()
        {
            var user = await _userManager.GetUserAsync(User);
            var courses = await _context.Courses
                .Where(c => c.Students.Any(s => s.Id == user.Id))
                .ToListAsync();

            return View("Index", courses);  // Reuse the Index view but with filtered data
        }

        // GET: Courses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .Include(c => c.Instructor)
                .Include(c => c.Students)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (course == null)
            {
                return NotFound();
            }

            // Check if user is admin, the instructor, or a student in this course
            if (!User.IsInRole("Admin"))
            {
                var user = await _userManager.GetUserAsync(User);
                if (course.Instructor.Id != user.Id &&
                    !course.Students.Any(s => s.Id == user.Id))
                {
                    return Forbid();
                }
            }

            return View(course);
        }

        // GET: Courses/Create
        public IActionResult Create()
        {
            ViewData["InstutionId"] = new SelectList(_context.Institution, "Id", "Name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,InstutionId,Term")] Course course)
        {
            try
            {
                // Set instructor ID
                var user = await _userManager.GetUserAsync(User);
                course.InstructorId = user.Id;

                // Add directly
                _context.Courses.Add(course);

                // Save with exception handling
                var result = await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Display detailed error info
                TempData["ErrorMessage"] = ex.Message;
                if (ex.InnerException != null)
                    TempData["InnerErrorMessage"] = ex.InnerException.Message;

                ViewData["InstutionId"] = new SelectList(_context.Institution, "Id", "Name", course.InstutionId);
                return View(course);
            }
        }

        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .Include(c => c.Instructor)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (course == null)
            {
                return NotFound();
            }

            // Check if user is admin or the instructor of this course
            if (!User.IsInRole("Admin"))
            {
                var user = await _userManager.GetUserAsync(User);
                if (course.Instructor.Id != user.Id)
                {
                    return Forbid();
                }
            }

            ViewData["InstutionId"] = new SelectList(_context.Institution, "Id", "Name", course.InstutionId);
            return View(course);
        }

        // POST: Courses/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,InstutionId,Term")] Course course)
        {
            if (id != course.Id)
            {
                return NotFound();
            }

            // Get the original course to check permissions and preserve the instructor
            var originalCourse = await _context.Courses
                .Include(c => c.Instructor)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (originalCourse == null)
            {
                return NotFound();
            }

            // Check if user is admin or the instructor of this course
            if (!User.IsInRole("Admin"))
            {
                var user = await _userManager.GetUserAsync(User);
                if (originalCourse.Instructor.Id != user.Id)
                {
                    return Forbid();
                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Preserve the original instructor
                    course.Instructor = originalCourse.Instructor;

                    _context.Entry(originalCourse).State = EntityState.Detached;
                    _context.Update(course);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["InstutionId"] = new SelectList(_context.Institution, "Id", "Name", course.InstutionId);
            return View(course);
        }

        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .Include(c => c.Instructor)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (course == null)
            {
                return NotFound();
            }

            // Check if user is admin or the instructor of this course
            if (!User.IsInRole("Admin"))
            {
                var user = await _userManager.GetUserAsync(User);
                if (course.Instructor.Id != user.Id)
                {
                    return Forbid();
                }
            }

            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await _context.Courses
                .Include(c => c.Instructor)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (course == null)
            {
                return NotFound();
            }

            // Check if user is admin or the instructor of this course
            if (!User.IsInRole("Admin"))
            {
                var user = await _userManager.GetUserAsync(User);
                if (course.Instructor.Id != user.Id)
                {
                    return Forbid();
                }
            }

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseExists(int id)
        {
            return _context.Courses.Any(e => e.Id == id);
        }
    }
}