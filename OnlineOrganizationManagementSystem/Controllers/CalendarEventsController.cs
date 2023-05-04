using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineOrganizationManagementSystem.Data;
using OnlineOrganizationManagementSystem.Models;

namespace OnlineOrganizationManagementSystem.Controllers
{
    public class CalendarEvents : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public CalendarEvents(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: CalendarEvents
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EventTable()
        {
            var holidays = await _context.CalendarEvent.ToListAsync();
            return View(holidays);
        }

        [Authorize(Roles = "Admin, User, Manager")]
        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);

            var currentTeams = await _context.Teams
                .Where(t => t.UIUXDeveloperId == currentUser.Id || t.FrontendDeveloperId == currentUser.Id || t.BackendDeveloperId == currentUser.Id || t.TesterId == currentUser.Id || t.TeamLeadId == currentUser.Id || t.ReportsToId == currentUser.Id)
                .ToListAsync();

            if (currentTeams == null)
            {
                var events = await _context.CalendarEvent.ToListAsync();
                var calendarItems = events.Select(e => new CalendarEvent
                {
                    Title = e.Title,
                    Date = e.Date,
                }).OrderBy(c => c.Date);

                return View(calendarItems);
            }
            else
            {
                var calendarItems = new List<CalendarEvent>();
                foreach (var currentTeam in currentTeams)
                {
                    var meetings = await _context.Meetings.Where(m => m.TeamId == currentTeam.Id).ToListAsync();
                    var allEvents = await _context.CalendarEvent.ToListAsync();

                    calendarItems.AddRange(meetings.Select(m => new CalendarEvent
                    {
                        Title = m.Title,
                        Date = m.Date,
                    }).Union(allEvents.Select(e => new CalendarEvent
                    {
                        Title = e.Title,
                        Date = e.Date,
                    })).OrderBy(c => c.Date));
                }
                return View(calendarItems);
            }
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