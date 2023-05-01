using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineOrganizationManagementSystem.Data;
using OnlineOrganizationManagementSystem.Models;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;

namespace OnlineOrganizationManagementSystem.Controllers
{
    public class TeamsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IEmailSender _emailSender;

        public TeamsController(ApplicationDbContext context, UserManager<IdentityUser> userManager, IEmailSender emailSender)
        {
            _context = context;
            _userManager = userManager;
            _emailSender = emailSender;
        }

        // GET: Teams
        [Authorize]
        public async Task<IActionResult> Index()
        {

            var currentUser = await _userManager.GetUserAsync(User);
            var currentTeam = await _context.Teams
            .Where(t => t.UIUXDeveloperId == currentUser.Id || t.FrontendDeveloperId == currentUser.Id || t.BackendDeveloperId == currentUser.Id || t.TesterId == currentUser.Id || t.TeamLeadId == currentUser.Id || t.ReportsToId == currentUser.Id).ToListAsync();
            //var applicationDbContext = _context.Tasks.Include(t => t.Assignee).Include(t => t.Manager).Include(t => t.Team);
            return View(currentTeam);

            //return _context.Teams != null ? 
            //              View(await _context.Teams.ToListAsync()) :
            //              Problem("Entity set 'ApplicationDbContext.Teams'  is null.");
        }

        // GET: Teams/Details/5
        [Authorize]
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
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> CreateAsync()
        {
            ViewData["BackendDeveloperId"] = new SelectList(await _userManager.GetUsersInRoleAsync("User"), "Id", "Email");
            ViewData["FrontendDeveloperId"] = new SelectList(await _userManager.GetUsersInRoleAsync("User"), "Id", "Email");
            ViewData["ReportsToId"] = new SelectList(await _userManager.GetUsersInRoleAsync("Manager"), "Id", "Email");
            ViewData["TeamLeadId"] = new SelectList(await _userManager.GetUsersInRoleAsync("User"), "Id", "Email");
            ViewData["TesterId"] = new SelectList(await _userManager.GetUsersInRoleAsync("User"), "Id", "Email");
            ViewData["UIUXDeveloperId"] = new SelectList(await _userManager.GetUsersInRoleAsync("User"), "Id", "Email");
            return View();
        }

        // POST: Teams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Manager")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,UIUXDeveloperId,FrontendDeveloperId,BackendDeveloperId,TesterId,TeamLeadId,ReportsToId,ProjectStatus")] Teams teams)
        {
           

            Console.WriteLine("Hit");
            if (ModelState.IsValid)
            {
                Console.WriteLine("Hit 1");
                _context.Add(teams);
                await _context.SaveChangesAsync();

                var UIUXDeveloperMail = await _userManager.FindByIdAsync(teams.UIUXDeveloperId);
                 var FrontendDeveloperMail = await _userManager.FindByIdAsync(teams.FrontendDeveloperId);
                 var BackendDeveloperMail = await _userManager.FindByIdAsync(teams.BackendDeveloperId);
                 var TesterMail = await _userManager.FindByIdAsync(teams.TesterId);
                 var ReportsToMail = await _userManager.FindByIdAsync(teams.ReportsToId);
                 var TeamLeadMail = await _userManager.FindByIdAsync(teams.TeamLeadId);

                await SendEmailAsync("krithikanithyanandam7@gmail.com", "You've been added to the Team!", $"You have been added to Team {teams.Name}\nReceiver {UIUXDeveloperMail.Email}");
                await SendEmailAsync("krithikanithyanandam7@gmail.com", "You've been added to the Team!", $"You have been added to Team {teams.Name}\nReceiver {FrontendDeveloperMail.Email}");
                await SendEmailAsync("krithikanithyanandam7@gmail.com", "You've been added to the Team!", $"You have been added to Team {teams.Name}\nReceiver {BackendDeveloperMail.Email}");
                await SendEmailAsync("krithikanithyanandam7@gmail.com", "You've been added to the Team!", $"You have been added to Team {teams.Name}\nReceiver {TesterMail.Email}");
                await SendEmailAsync("krithikanithyanandam7@gmail.com", "You've been added to the Team!", $"You have been added to Team {teams.Name}\nReceiver {ReportsToMail.Email}");
                await SendEmailAsync("krithikanithyanandam7@gmail.com", "You've been added to the Team!", $"You have been added to Team {teams.Name}\nReceiver {TeamLeadMail.Email}");


                
                return RedirectToAction(nameof(Index));
            }
            ViewData["BackendDeveloperId"] = new SelectList(await _userManager.GetUsersInRoleAsync("User"), "Id", "Email", teams.BackendDeveloperId);
            ViewData["FrontendDeveloperId"] = new SelectList(await _userManager.GetUsersInRoleAsync("User"), "Id", "Email", teams.FrontendDeveloperId);
            ViewData["ReportsToId"] = new SelectList(await _userManager.GetUsersInRoleAsync("Manager"), "Id", "Email", teams.ReportsToId);
            ViewData["TeamLeadId"] = new SelectList(await _userManager.GetUsersInRoleAsync("User"), "Id", "Email", teams.TeamLeadId);
            ViewData["TesterId"] = new SelectList(await _userManager.GetUsersInRoleAsync("User"), "Id", "Email", teams.TesterId);
            ViewData["UIUXDeveloperId"] = new SelectList(await _userManager.GetUsersInRoleAsync("User"), "Id", "Email", teams.UIUXDeveloperId);
            return View(teams);
        }


        // GET: Teams/Edit/5
        [Authorize(Roles = "Manager")]
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
            ViewData["BackendDeveloperId"] = new SelectList(await _userManager.GetUsersInRoleAsync("User"), "Id", "Email", teams.BackendDeveloperId);
            ViewData["FrontendDeveloperId"] = new SelectList(await _userManager.GetUsersInRoleAsync("User"), "Id", "Email", teams.FrontendDeveloperId);
            ViewData["ReportsToId"] = new SelectList(await _userManager.GetUsersInRoleAsync("Manager"), "Id", "Email", teams.ReportsToId);
            ViewData["TeamLeadId"] = new SelectList(await _userManager.GetUsersInRoleAsync("User"), "Id", "Email", teams.TeamLeadId);
            ViewData["TesterId"] = new SelectList(await _userManager.GetUsersInRoleAsync("User"), "Id", "Email", teams.TesterId);
            ViewData["UIUXDeveloperId"] = new SelectList(await _userManager.GetUsersInRoleAsync("User"), "Id", "Email", teams.UIUXDeveloperId);
            return View(teams);
        }

        // POST: Teams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Manager")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,UIUXDeveloperId,FrontendDeveloperId,BackendDeveloperId,TesterId,TeamLeadId,ReportsToId,ProjectStatus")] Teams teams)
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
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Close(int? id)
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
            ViewData["BackendDeveloperId"] = new SelectList(await _userManager.GetUsersInRoleAsync("User"), "Id", "Email", teams.BackendDeveloperId);
            ViewData["FrontendDeveloperId"] = new SelectList(await _userManager.GetUsersInRoleAsync("User"), "Id", "Email", teams.FrontendDeveloperId);
            ViewData["ReportsToId"] = new SelectList(await _userManager.GetUsersInRoleAsync("Manager"), "Id", "Email", teams.ReportsToId);
            ViewData["TeamLeadId"] = new SelectList(await _userManager.GetUsersInRoleAsync("User"), "Id", "Email", teams.TeamLeadId);
            ViewData["TesterId"] = new SelectList(await _userManager.GetUsersInRoleAsync("User"), "Id", "Email", teams.TesterId);
            ViewData["UIUXDeveloperId"] = new SelectList(await _userManager.GetUsersInRoleAsync("User"), "Id", "Email", teams.UIUXDeveloperId);
            return View(teams);
        }

        //Archive Project
        [Authorize(Roles = "Manager")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Close(int id, [Bind("Id,Name,Description,UIUXDeveloperId,FrontendDeveloperId,BackendDeveloperId,TesterId,TeamLeadId,ReportsToId,ProjectStatus")] Teams teams)
        {
            if (id != teams.Id)
            {
                return NotFound();
            }

            Console.WriteLine($"Team Name: {teams.Name}\n" +
            $"Team Description: {teams.Description}\n" +
            $"UI/UX Developer Id: {teams.UIUXDeveloperId}\n" +
            $"Frontend Developer Id: {teams.FrontendDeveloperId}\n" +
            $"Backend Developer Id: {teams.BackendDeveloperId}\n" +
            $"Tester Id: {teams.TesterId}\n" +
            $"Team Lead Id: {teams.TeamLeadId}\n" +
            $"Reports To Id: {teams.ReportsToId}\n" +
            $"Project Status: {teams.ProjectStatus}");
            var archive = new Archives
            {
                TeamId = teams.Id,
                Name = teams.Name,
                Description = teams.Description,
                UIUXDeveloperId = teams.UIUXDeveloperId,
                FrontendDeveloperId = teams.FrontendDeveloperId,
                BackendDeveloperId = teams.BackendDeveloperId,
                TesterId = teams.TesterId,
                TeamLeadId = teams.TeamLeadId,
                ReportsToId = teams.ReportsToId,
                ProjectStatus = "Completed",
                DateArchived = DateTime.Now
            };
            

                try
                {
                    _context.Archives.Add(archive);
                    await _context.SaveChangesAsync();
                    var receiver = await _userManager.FindByIdAsync(teams.ReportsToId);

                    Console.WriteLine(receiver.Email);
                    //await _emailSender.SendEmailAsync("krithika.23it@licet.ac.in", "Project Archived",
                    //$"Your project with {teams.Name} has been archived");
                    await SendEmailAsync("krithikanithyanandam7@gmail.com", "Project Archived", $"Your project with {teams.Name} has been archived\nReceiver Mail: {receiver}");

                    _context.Teams.Remove(teams);
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

        // GET: Teams/Delete/5
        [Authorize(Roles = "Manager")]
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
        [Authorize(Roles = "Manager")]
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

        // Retrieve Archives
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Archival()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var archives = await _context.Archives.Where(a => a.ReportsToId == userId).ToListAsync();
            return View(archives);
        }

       // Send Mail Function
      

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
    private bool TeamsExists(int id)
        {
          return (_context.Teams?.Any(e => e.Id == id)).GetValueOrDefault();
        }


    }
}
