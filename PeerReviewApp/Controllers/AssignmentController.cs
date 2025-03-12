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
    [Authorize]
    public class AssignmentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public AssignmentController(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Assignment
        public async Task<IActionResult> Index()
        {
            // If admin, show all assignments
            if (User.IsInRole("Admin"))
            {
                return View(await _context.Assignments.ToListAsync());
            }

            // If instructor, show only their course assignments
            if (User.IsInRole("Instructor"))
            {
                var user = await _userManager.GetUserAsync(User);
                var instructorCourses = await _context.Courses
                    .Where(c => c.Instructor.Id == user.Id)
                    .Select(c => c.Id.ToString())
                    .ToListAsync();

                var assignments = await _context.Assignments
                    .Where(a => instructorCourses.Contains(a.CourseId))
                    .ToListAsync();

                return View(assignments);
            }

            // If student, redirect to student assignments
            return RedirectToAction(nameof(StudentAssignments));
        }

        // GET: Assignment/StudentAssignments
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> StudentAssignments()
        {
            var user = await _userManager.GetUserAsync(User);
            var studentCourses = await _context.Courses
                .Where(c => c.Students.Any(s => s.Id == user.Id))
                .Select(c => c.Id.ToString())
                .ToListAsync();

            var assignments = await _context.Assignments
                .Where(a => studentCourses.Contains(a.CourseId))
                .ToListAsync();

            return View("Index", assignments); // Reuse the Index view with filtered data
        }

        // GET: Assignment/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assignment = await _context.Assignments
                .FirstOrDefaultAsync(m => m.Id == id);

            if (assignment == null)
            {
                return NotFound();
            }

            // Check access permissions based on role
            if (!User.IsInRole("Admin"))
            {
                var user = await _userManager.GetUserAsync(User);
                var course = await _context.Courses
                    .Include(c => c.Instructor)
                    .Include(c => c.Students)
                    .FirstOrDefaultAsync(c => c.Id.ToString() == assignment.CourseId);

                if (course == null)
                {
                    return NotFound();
                }

                // Check if user is instructor or student of this course
                if (course.Instructor.Id != user.Id &&
                    !course.Students.Any(s => s.Id == user.Id))
                {
                    return Forbid();
                }
            }

            return View(assignment);
        }

        // GET: Assignment/Create
        [Authorize(Roles = "Instructor,Admin")]
        public IActionResult Create()
        {
            // For instructors, only show their courses
            if (User.IsInRole("Instructor"))
            {
                var userId = _userManager.GetUserId(User);
                var instructorCourses = _context.Courses
                    .Where(c => c.Instructor.Id == userId)
                    .ToList();

                ViewData["CourseId"] = new SelectList(instructorCourses, "Id", "Name");
            }
            else // For admins, show all courses
            {
                ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name");
            }

            return View();
        }

        // POST: Assignment/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Instructor,Admin")]
        public async Task<IActionResult> Create([Bind("Id,CourseId,DueDate,Title,Description,FilePath")] Assignment assignment)
        {
            // Check if user has permission to create assignments for this course
            if (!User.IsInRole("Admin"))
            {
                var user = await _userManager.GetUserAsync(User);
                var course = await _context.Courses
                    .Include(c => c.Instructor)
                    .FirstOrDefaultAsync(c => c.Id.ToString() == assignment.CourseId);

                if (course == null || course.Instructor.Id != user.Id)
                {
                    return Forbid();
                }
            }

            if (ModelState.IsValid)
            {
                _context.Add(assignment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Repopulate course dropdown
            if (User.IsInRole("Instructor"))
            {
                var userId = _userManager.GetUserId(User);
                var instructorCourses = _context.Courses
                    .Where(c => c.Instructor.Id == userId)
                    .ToList();

                ViewData["CourseId"] = new SelectList(instructorCourses, "Id", "Name", assignment.CourseId);
            }
            else
            {
                ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name", assignment.CourseId);
            }

            return View(assignment);
        }

        // GET: Assignment/Edit/5
        [Authorize(Roles = "Instructor,Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assignment = await _context.Assignments.FindAsync(id);
            if (assignment == null)
            {
                return NotFound();
            }

            // Check if user has permission to edit this assignment
            if (!User.IsInRole("Admin"))
            {
                var user = await _userManager.GetUserAsync(User);
                var course = await _context.Courses
                    .Include(c => c.Instructor)
                    .FirstOrDefaultAsync(c => c.Id.ToString() == assignment.CourseId);

                if (course == null || course.Instructor.Id != user.Id)
                {
                    return Forbid();
                }
            }

            // Prepare course dropdown
            if (User.IsInRole("Instructor"))
            {
                var userId = _userManager.GetUserId(User);
                var instructorCourses = _context.Courses
                    .Where(c => c.Instructor.Id == userId)
                    .ToList();

                ViewData["CourseId"] = new SelectList(instructorCourses, "Id", "Name", assignment.CourseId);
            }
            else
            {
                ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name", assignment.CourseId);
            }

            return View(assignment);
        }

        // POST: Assignment/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Instructor,Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CourseId,DueDate,Title,Description,FilePath")] Assignment assignment)
        {
            if (id != assignment.Id)
            {
                return NotFound();
            }

            // Check if user has permission to edit this assignment
            if (!User.IsInRole("Admin"))
            {
                var user = await _userManager.GetUserAsync(User);
                var course = await _context.Courses
                    .Include(c => c.Instructor)
                    .FirstOrDefaultAsync(c => c.Id.ToString() == assignment.CourseId);

                if (course == null || course.Instructor.Id != user.Id)
                {
                    return Forbid();
                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(assignment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssignmentExists(assignment.Id))
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

            // Repopulate course dropdown
            if (User.IsInRole("Instructor"))
            {
                var userId = _userManager.GetUserId(User);
                var instructorCourses = _context.Courses
                    .Where(c => c.Instructor.Id == userId)
                    .ToList();

                ViewData["CourseId"] = new SelectList(instructorCourses, "Id", "Name", assignment.CourseId);
            }
            else
            {
                ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name", assignment.CourseId);
            }

            return View(assignment);
        }

        // GET: Assignment/Delete/5
        [Authorize(Roles = "Instructor,Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assignment = await _context.Assignments
                .FirstOrDefaultAsync(m => m.Id == id);

            if (assignment == null)
            {
                return NotFound();
            }

            // Check if user has permission to delete this assignment
            if (!User.IsInRole("Admin"))
            {
                var user = await _userManager.GetUserAsync(User);
                var course = await _context.Courses
                    .Include(c => c.Instructor)
                    .FirstOrDefaultAsync(c => c.Id.ToString() == assignment.CourseId);

                if (course == null || course.Instructor.Id != user.Id)
                {
                    return Forbid();
                }
            }

            return View(assignment);
        }

        // POST: Assignment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Instructor,Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var assignment = await _context.Assignments.FindAsync(id);
            if (assignment == null)
            {
                return NotFound();
            }

            // Check if user has permission to delete this assignment
            if (!User.IsInRole("Admin"))
            {
                var user = await _userManager.GetUserAsync(User);
                var course = await _context.Courses
                    .Include(c => c.Instructor)
                    .FirstOrDefaultAsync(c => c.Id.ToString() == assignment.CourseId);

                if (course == null || course.Instructor.Id != user.Id)
                {
                    return Forbid();
                }
            }

            _context.Assignments.Remove(assignment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AssignmentExists(int id)
        {
            return _context.Assignments.Any(e => e.Id == id);
        }
    }
}