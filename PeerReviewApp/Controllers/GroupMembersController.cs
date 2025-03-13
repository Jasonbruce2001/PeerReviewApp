using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PeerReviewApp.Data;
using PeerReviewApp.Models;

namespace PeerReviewApp.Controllers
{
    public class GroupMembersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GroupMembersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: GroupMembers
        public async Task<IActionResult> Index()
        {
            return View(await _context.GroupMembers.ToListAsync());
        }

        // GET: GroupMembers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groupMembers = await _context.GroupMembers
                .FirstOrDefaultAsync(m => m.GroupMembersId == id);
            if (groupMembers == null)
            {
                return NotFound();
            }

            return View(groupMembers);
        }

        // GET: GroupMembers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GroupMembers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GroupMembersId")] GroupMembers groupMembers)
        {
            if (ModelState.IsValid)
            {
                _context.Add(groupMembers);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(groupMembers);
        }

        // GET: GroupMembers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groupMembers = await _context.GroupMembers.FindAsync(id);
            if (groupMembers == null)
            {
                return NotFound();
            }
            return View(groupMembers);
        }

        // POST: GroupMembers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GroupMembersId")] GroupMembers groupMembers)
        {
            if (id != groupMembers.GroupMembersId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(groupMembers);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GroupMembersExists(groupMembers.GroupMembersId))
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
            return View(groupMembers);
        }

        // GET: GroupMembers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groupMembers = await _context.GroupMembers
                .FirstOrDefaultAsync(m => m.GroupMembersId == id);
            if (groupMembers == null)
            {
                return NotFound();
            }

            return View(groupMembers);
        }

        // POST: GroupMembers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var groupMembers = await _context.GroupMembers.FindAsync(id);
            if (groupMembers != null)
            {
                _context.GroupMembers.Remove(groupMembers);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GroupMembersExists(int id)
        {
            return _context.GroupMembers.Any(e => e.GroupMembersId == id);
        }
    }
}
