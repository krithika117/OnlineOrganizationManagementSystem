using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineOrganizationManagementSystem.Data;
using OnlineOrganizationManagementSystem.Models;

namespace OnlineOrganizationManagementSystem.Controllers
{
    public class AttendanceRecordsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;


        public AttendanceRecordsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: AttendanceRecords
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var attendanceRecords = await _context.AttendanceRecord
                .Where(n => n.UserId == user.Id)
                .ToListAsync();
            var today = DateTime.Today;
            var attendanceRecordExists = await _context.AttendanceRecord
                .AnyAsync(n => n.UserId == user.Id && n.DateRecord.Date == today);
            Console.WriteLine(DateTime.Today);
            Console.WriteLine(attendanceRecordExists);
            ViewData["AttendanceRecordExists"] = attendanceRecordExists;

            return View(attendanceRecords);
        }

        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> GetTeamAttendance()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var attendanceRecords = await _context.AttendanceRecord
                .Where(n => n.UserId == currentUser.Id)
                .ToListAsync();
            ViewData["Title"] = "Attendance Records";
            ViewBag.AttendanceTeamId = new SelectList(_context.Teams, "Id", "Name");

            var currentTeam = await _context.Teams
           .FirstOrDefaultAsync(t => t.ReportsToId == currentUser.Id);

            return View(attendanceRecords);
        }

        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> TeamAttendancePartial(int InTeamId)
        {
           
            var team = await _context.Teams.FirstOrDefaultAsync(n => n.Id == InTeamId);

            var teamMembers = new[] { team.UIUXDeveloperId,
            team.FrontendDeveloperId, team.BackendDeveloperId, team.TesterId, team.TeamLeadId, team.TeamLeadId};

            Console.WriteLine(teamMembers);

            var attendanceRecords = await _context.AttendanceRecord
            .Where(r => teamMembers.Contains(r.UserId))
            .ToListAsync();
            Console.WriteLine(attendanceRecords[0].PresenceStatus);
            return PartialView("TeamAttendancePartial", attendanceRecords);

        }

        // GET: AttendanceRecords/Details/5
        [Authorize]
            public async Task<IActionResult> Details(int? id)
            {
                if (id == null || _context.AttendanceRecord == null)
                {
                    return NotFound();
                }

                var attendanceRecord = await _context.AttendanceRecord
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (attendanceRecord == null)
                {
                    return NotFound();
                }

                return View(attendanceRecord);
            }

            // GET: AttendanceRecords/Create
            [Authorize]
            public IActionResult Create()
            {
                return View();
            }

            // POST: AttendanceRecords/Create
            // To protect from overposting attacks, enable the specific properties you want to bind to.
            // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [Authorize]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create([Bind("Id,PresenceStatus,Description,DateRecord,UserId")] AttendanceRecord attendanceRecord)
            {
                if (ModelState.IsValid)
                {
                    _context.Add(attendanceRecord);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(attendanceRecord);
            }

            // GET: AttendanceRecords/Edit/5
            //public async Task<IActionResult> Edit(int? id)
            //{
            //    if (id == null || _context.AttendanceRecord == null)
            //    {
            //        return NotFound();
            //    }

            //    var attendanceRecord = await _context.AttendanceRecord.FindAsync(id);
            //    if (attendanceRecord == null)
            //    {
            //        return NotFound();
            //    }
            //    return View(attendanceRecord);
            //}

            // POST: AttendanceRecords/Edit/5
            // To protect from overposting attacks, enable the specific properties you want to bind to.
            // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

            //[HttpPost]
            //[ValidateAntiForgeryToken]
            //public async Task<IActionResult> Edit(int id, [Bind("Id,PresenceStatus,Description,DateRecord,UserId")] AttendanceRecord attendanceRecord)
            //{
            //    if (id != attendanceRecord.Id)
            //    {
            //        return NotFound();
            //    }

            //    if (ModelState.IsValid)
            //    {
            //        try
            //        {
            //            _context.Update(attendanceRecord);
            //            await _context.SaveChangesAsync();
            //        }
            //        catch (DbUpdateConcurrencyException)
            //        {
            //            if (!AttendanceRecordExists(attendanceRecord.Id))
            //            {
            //                return NotFound();
            //            }
            //            else
            //            {
            //                throw;
            //            }
            //        }
            //        return RedirectToAction(nameof(Index));
            //    }
            //    return View(attendanceRecord);
            //}

            // GET: AttendanceRecords/Delete/5
            //public async Task<IActionResult> Delete(int? id)
            //{
            //    if (id == null || _context.AttendanceRecord == null)
            //    {
            //        return NotFound();
            //    }

            //    var attendanceRecord = await _context.AttendanceRecord
            //        .FirstOrDefaultAsync(m => m.Id == id);
            //    if (attendanceRecord == null)
            //    {
            //        return NotFound();
            //    }

            //    return View(attendanceRecord);
            //}

            // POST: AttendanceRecords/Delete/5
            //[HttpPost, ActionName("Delete")]
            //[ValidateAntiForgeryToken]
            //public async Task<IActionResult> DeleteConfirmed(int id)
            //{
            //    if (_context.AttendanceRecord == null)
            //    {
            //        return Problem("Entity set 'ApplicationDbContext.AttendanceRecord'  is null.");
            //    }
            //    var attendanceRecord = await _context.AttendanceRecord.FindAsync(id);
            //    if (attendanceRecord != null)
            //    {
            //        _context.AttendanceRecord.Remove(attendanceRecord);
            //    }

            //    await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //}

            private bool AttendanceRecordExists(int id)
            {
                return (_context.AttendanceRecord?.Any(e => e.Id == id)).GetValueOrDefault();
            }
        }
    }
