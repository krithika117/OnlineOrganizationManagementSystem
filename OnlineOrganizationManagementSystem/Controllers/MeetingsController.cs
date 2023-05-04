using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using NuGet.Protocol.Plugins;
using OnlineOrganizationManagementSystem.Data;
using OnlineOrganizationManagementSystem.Models;
using Org.BouncyCastle.Utilities.IO;

namespace OnlineOrganizationManagementSystem.Controllers
{
    public class MeetingsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public MeetingsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Meetings
        [Authorize(Roles = "User, Manager")]
        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);

            var currentTeams = await _context.Teams
                 .Where(t => t.UIUXDeveloperId == currentUser.Id || t.FrontendDeveloperId == currentUser.Id || t.BackendDeveloperId == currentUser.Id || t.TesterId == currentUser.Id || t.TeamLeadId == currentUser.Id || t.ReportsToId == currentUser.Id)
                 .ToListAsync();
            Console.WriteLine("Called Team Index");
            var events = new List<Meeting>();
            foreach (var currentTeam in currentTeams)
            {
                events.AddRange(await _context.Meetings.Where(m => m.TeamId == currentTeam.Id).ToListAsync());
            }
            return View(events);
          
        }

        // GET: Meetings/Details/5
        [Authorize(Roles = "User, Manager")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Meetings == null)
            {
                return NotFound();
            }

            var meeting = await _context.Meetings
              .Include(m => m.Team)
              .FirstOrDefaultAsync(m => m.Id == id);
            if (meeting == null)
            {
                return NotFound();
            }

            return View(meeting);
        }

        // GET: Meetings/Create
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> CreateAsync()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            ViewBag.MeetTeamId = new SelectList(_context.Teams.Where(n => n.ReportsToId == currentUser.Id), "Id", "Name");
            return View();
        }

        // POST: Meetings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Manager")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EventType,Title,Details,TeamId,Date")] Meeting meeting)
        {
            var team = await _context.Teams.FindAsync(meeting.TeamId);

            if (team == null)
            {
                return NotFound();
            }

            // Get the user emails
            var users = await _context.Users
                .Where(u => u.Id == team.UIUXDeveloperId
                         || u.Id == team.FrontendDeveloperId
                         || u.Id == team.BackendDeveloperId
                         || u.Id == team.TesterId
                         || u.Id == team.TeamLeadId
                         || u.Id == team.ReportsToId)
                .ToListAsync();
            _context.Add(meeting);
            await _context.SaveChangesAsync();
           

            foreach (var user in users)
            {
                await SendEmailAsync("krithikanithyanandam7@gmail.com", "Meeting Scheduled", $"Your meeting has been scheduled for {meeting.Title} on {meeting.Date} \nReceiver Mail: {user.Email}");
                
            }
            return RedirectToAction(nameof(Index));

        }


        // Mail server
        public async Task SendEmailAsync(string recipientEmail, string subject, string messageBody)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("promote.n0replymailer@gmail.com", "promote.n0replymailer@gmail.com"));
            message.To.Add(new MailboxAddress("Admin", recipientEmail));
            message.Subject = subject;

            message.Body = new TextPart("plain")
            {
                Text = messageBody
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.gmail.com", 587, false);
                await client.AuthenticateAsync("promote.n0replymailer@gmail.com", "ashqtcouelkfiznr");
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }

        // GET: Meetings/Edit/5
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Meetings == null)
            {
                return NotFound();
            }

            var meeting = await _context.Meetings.FindAsync(id);
            if (meeting == null)
            {
                return NotFound();
            }
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Name", meeting.TeamId);
            return View(meeting);
        }

        // POST: Meetings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Manager")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EventType,Title,Details,TeamId,Date")] Meeting meeting)
        {
            if (id != meeting.Id)
            {
                return NotFound();
            }

            try
            {
                _context.Update(meeting);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MeetingExists(meeting.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));

            //ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Name", meeting.TeamId);
            //return View(meeting);
        }

        // GET: Meetings/Delete/5
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Meetings == null)
            {
                return NotFound();
            }

            var meeting = await _context.Meetings
              .Include(m => m.Team)
              .FirstOrDefaultAsync(m => m.Id == id);
            if (meeting == null)
            {
                return NotFound();
            }

            return View(meeting);
        }

        // POST: Meetings/Delete/5
        [Authorize(Roles = "Manager")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Meetings == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Meetings'  is null.");
            }
            var meeting = await _context.Meetings.FindAsync(id);
            if (meeting != null)
            {
                _context.Meetings.Remove(meeting);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MeetingExists(int id)
        {
            return (_context.Meetings?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}