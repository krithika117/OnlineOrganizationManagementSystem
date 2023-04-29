using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineOrganizationManagementSystem.Data;
using OnlineOrganizationManagementSystem.Models;

namespace OnlineOrganizationManagementSystem.Controllers
{
    public class PublicHolidaysController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PublicHolidaysController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PublicHolidays
        [Authorize(Roles = "Admin, User, Manager")]
        public async Task<IActionResult> Index()
        {
            return _context.PublicHoliday != null ?
                        View(await _context.PublicHoliday.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.PublicHoliday'  is null.");
        }

        // GET: PublicHolidays/Details/5
        [Authorize(Roles = "Admin, User, Manager")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PublicHoliday == null)
            {
                return NotFound();
            }

            var publicHoliday = await _context.PublicHoliday
                .FirstOrDefaultAsync(m => m.Id == id);
            if (publicHoliday == null)
            {
                return NotFound();
            }

            return View(publicHoliday);
        }

        // GET: PublicHolidays/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: PublicHolidays/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Date")] PublicHoliday publicHoliday)
        {
            if (ModelState.IsValid)
            {
                _context.Add(publicHoliday);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(publicHoliday);
        }

        // GET: PublicHolidays/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PublicHoliday == null)
            {
                return NotFound();
            }

            var publicHoliday = await _context.PublicHoliday.FindAsync(id);
            if (publicHoliday == null)
            {
                return NotFound();
            }
            return View(publicHoliday);
        }

        // POST: PublicHolidays/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Date")] PublicHoliday publicHoliday)
        {
            if (id != publicHoliday.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(publicHoliday);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PublicHolidayExists(publicHoliday.Id))
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
            return View(publicHoliday);
        }

        // GET: PublicHolidays/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PublicHoliday == null)
            {
                return NotFound();
            }

            var publicHoliday = await _context.PublicHoliday
                .FirstOrDefaultAsync(m => m.Id == id);
            if (publicHoliday == null)
            {
                return NotFound();
            }

            return View(publicHoliday);
        }

        // POST: PublicHolidays/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PublicHoliday == null)
            {
                return Problem("Entity set 'ApplicationDbContext.PublicHoliday'  is null.");
            }
            var publicHoliday = await _context.PublicHoliday.FindAsync(id);
            if (publicHoliday != null)
            {
                _context.PublicHoliday.Remove(publicHoliday);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PublicHolidayExists(int id)
        {
            return (_context.PublicHoliday?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}