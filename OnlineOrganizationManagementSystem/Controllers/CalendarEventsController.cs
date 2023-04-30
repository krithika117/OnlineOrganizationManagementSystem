using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineOrganizationManagementSystem.Data;
using OnlineOrganizationManagementSystem.Models;

namespace OnlineOrganizationManagementSystem.Controllers
{
    public class CalendarEvents : Controller
    {
        private readonly ApplicationDbContext _context;

        public CalendarEvents(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CalendarEvents
        [Authorize(Roles = "Admin, User, Manager")]
        public async Task<IActionResult> Index()
        {
            return _context.CalendarEvent != null ?
                        View(await _context.CalendarEvent.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.CalendarEvent'  is null.");
        }

        // GET: CalendarEvents/Details/5
        [Authorize(Roles = "Admin, User, Manager")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CalendarEvent == null)
            {
                return NotFound();
            }

            var CalendarEvent = await _context.CalendarEvent
                .FirstOrDefaultAsync(m => m.Id == id);
            if (CalendarEvent == null)
            {
                return NotFound();
            }

            return View(CalendarEvent);
        }

        // GET: CalendarEvents/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: CalendarEvents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Date")] CalendarEvent CalendarEvent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(CalendarEvent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(CalendarEvent);
        }

        // GET: CalendarEvents/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CalendarEvent == null)
            {
                return NotFound();
            }

            var CalendarEvent = await _context.CalendarEvent.FindAsync(id);
            if (CalendarEvent == null)
            {
                return NotFound();
            }
            return View(CalendarEvent);
        }

        // POST: CalendarEvents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Date")] CalendarEvent CalendarEvent)
        {
            if (id != CalendarEvent.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(CalendarEvent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CalendarEventExists(CalendarEvent.Id))
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
            return View(CalendarEvent);
        }

        // GET: CalendarEvents/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CalendarEvent == null)
            {
                return NotFound();
            }

            var CalendarEvent = await _context.CalendarEvent
                .FirstOrDefaultAsync(m => m.Id == id);
            if (CalendarEvent == null)
            {
                return NotFound();
            }

            return View(CalendarEvent);
        }

        // POST: CalendarEvents/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CalendarEvent == null)
            {
                return Problem("Entity set 'ApplicationDbContext.CalendarEvent'  is null.");
            }
            var CalendarEvent = await _context.CalendarEvent.FindAsync(id);
            if (CalendarEvent != null)
            {
                _context.CalendarEvent.Remove(CalendarEvent);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CalendarEventExists(int id)
        {
            return (_context.CalendarEvent?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}