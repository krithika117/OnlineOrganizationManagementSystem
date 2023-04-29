using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineOrganizationManagementSystem.Data;

namespace OnlineOrganizationManagementSystem.Controllers
{
    public class TeamsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TeamsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Teams
        public async Task<IActionResult> Index()
        {
              return _context.Teams != null ? 
                          View(await _context.Teams.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Teams'  is null.");
        }

        // GET: Teams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Teams == null)
            {
                return NotFound();
            }

            var teams = await _context.Teams
                .FirstOrDefaultAsync(m => m.Id == id);
            if (teams == null)
            {
                return NotFound();
            }

            return View(teams);
        }

        // GET: Teams/Create
        public IActionResult Create()
        {
            ViewData["BackendDeveloperId"] = new SelectList(_context.Users, "Id", "Email");
            ViewData["FrontendDeveloperId"] = new SelectList(_context.Users, "Id", "Email");
            ViewData["ReportsToId"] = new SelectList(_context.Users, "Id", "Email");
            ViewData["TeamLeadId"] = new SelectList(_context.Users, "Id", "Email");
            ViewData["TesterId"] = new SelectList(_context.Users, "Id", "Email");
            ViewData["UIUXDeveloperId"] = new SelectList(_context.Users, "Id", "Email");
            return View();
        }

        // POST: Teams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,UIUXDeveloperId,FrontendDeveloperId,BackendDeveloperId,TesterId,TeamLeadId,ReportsToId")] Teams teams)
        {
            foreach (var entry in ModelState)
            {
                var key = entry.Key;
                var errors = entry.Value.Errors;

                foreach (var error in errors)
                {
                    var errorMessage = error.ErrorMessage;
                    var exception = error.Exception;

                    Console.WriteLine(errorMessage);
                    Console.WriteLine(exception);
                }
            }

            Console.WriteLine("Hit");
            if (ModelState.IsValid)
            {
                Console.WriteLine("Hit 1");
                _context.Add(teams);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BackendDeveloperId"] = new SelectList(_context.Users, "Id", "Email", teams.BackendDeveloperId);
            ViewData["FrontendDeveloperId"] = new SelectList(_context.Users, "Id", "Email", teams.FrontendDeveloperId);
            ViewData["ReportsToId"] = new SelectList(_context.Users, "Id", "Email", teams.ReportsToId);
            ViewData["TeamLeadId"] = new SelectList(_context.Users, "Id", "Email", teams.TeamLeadId);
            ViewData["TesterId"] = new SelectList(_context.Users, "Id", "Email", teams.TesterId);
            ViewData["UIUXDeveloperId"] = new SelectList(_context.Users, "Id", "Email", teams.UIUXDeveloperId);
            return View(teams);
        }


        // GET: Teams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Teams == null)
            {
                return NotFound();
            }

            var teams = await _context.Teams.FindAsync(id);
            if (teams == null)
            {
                return NotFound();
            }
            ViewData["BackendDeveloperId"] = new SelectList(_context.Users, "Id", "Email", teams.BackendDeveloperId);
            ViewData["FrontendDeveloperId"] = new SelectList(_context.Users, "Id", "Email", teams.FrontendDeveloperId);
            ViewData["ReportsToId"] = new SelectList(_context.Users, "Id", "Email", teams.ReportsToId);
            ViewData["TeamLeadId"] = new SelectList(_context.Users, "Id", "Email", teams.TeamLeadId);
            ViewData["TesterId"] = new SelectList(_context.Users, "Id", "Email", teams.TesterId);
            ViewData["UIUXDeveloperId"] = new SelectList(_context.Users, "Id", "Email", teams.UIUXDeveloperId);
            return View(teams);
        }

        // POST: Teams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,UIUXDeveloperId,FrontendDeveloperId,BackendDeveloperId,TesterId,TeamLeadId,ReportsToId")] Teams teams)
        {
            if (id != teams.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teams);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeamsExists(teams.Id))
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
            return View(teams);
        }

        // GET: Teams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Teams == null)
            {
                return NotFound();
            }

            var teams = await _context.Teams
                .FirstOrDefaultAsync(m => m.Id == id);
            if (teams == null)
            {
                return NotFound();
            }

            return View(teams);
        }

        // POST: Teams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Teams == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Teams'  is null.");
            }
            var teams = await _context.Teams.FindAsync(id);
            if (teams != null)
            {
                _context.Teams.Remove(teams);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeamsExists(int id)
        {
          return (_context.Teams?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
