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
    public class TasksController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public TasksController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Tasks
        // Check if task is overdue        
        public IActionResult UpdateTaskStatus()
        {
            try
            {
                var tasks = _context.Tasks.Where(t => (t.Status == "Assigned")).ToList();
                var tasks1 = _context.Tasks.ToList();
          
                foreach (var task in tasks)
                {
                    if (task.DueDate < DateTime.Now)
                    {
                        task.Status = "Overdue";
                        _context.Tasks.Update(task);
                    }
                    _context.SaveChanges();
                }
           
           
                
                return Ok();
            }
            catch(Exception e){ return BadRequest(); }
            
        }

        [Authorize]
        
        public async Task<IActionResult> Index()
        {
            
            UpdateTaskStatus();
            var currentUser = await _userManager.GetUserAsync(User);
    
            ViewData["TeamId"] = new SelectList(_context.Teams
            .Where(t => t.UIUXDeveloperId == currentUser.Id || t.FrontendDeveloperId == currentUser.Id || t.BackendDeveloperId == currentUser.Id || t.TesterId == currentUser.Id || t.TeamLeadId == currentUser.Id || t.ReportsToId == currentUser.Id), "Id", "Name");
            
            var currUserTasks = await _context.Tasks
                   .Where(n => n.AssigneeId == currentUser.Id || n.ManagerId == currentUser.Id)
                   .ToListAsync();
           

            return View(currUserTasks);
        }

        [Authorize]
        
        public async Task<IActionResult> GetTasks(int TeamId) {
            var currentUser = await _userManager.GetUserAsync(User);

            var currRetTasks = await _context.Tasks
                .Where(n => (n.AssigneeId == currentUser.Id || n.ManagerId == currentUser.Id) && n.TeamId == TeamId)
                .ToListAsync();
            Console.WriteLine("Tasks retrieved");
            Console.WriteLine(currRetTasks);
            return PartialView("GetTasks", currRetTasks);
        }

        // GET: Tasks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Tasks == null)
            {
                return NotFound();
            }

            var tasks = await _context.Tasks
                .Include(t => t.Assignee)
                .Include(t => t.Manager)
                .Include(t => t.Team)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tasks == null)
            {
                return NotFound();
            }

            return View(tasks);
        }

        // GET: Tasks/Create
        public async Task<IActionResult> Create()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            ViewData["AssigneeId"] = new SelectList(_context.Users, "Id", "Email");
            ViewData["ManagerId"] = new SelectList(_context.Users, "Id", "Email");
            ViewData["TeamId"] = new SelectList(_context.Teams.Where(t => t.ReportsToId == currentUser.Id), "Id", "Name");
            return View();
        }


        public IActionResult GetAssigneesForTeam(int teamId)
        {
            Console.WriteLine("GetAssigneesForTeam Team ID " + teamId);
            var team = _context.Teams.FirstOrDefault(t => t.Id == teamId);

            List<SelectListItem> assignees = new List<SelectListItem>();

            // Add each assignee to the list
            if (team.UIUXDeveloperId != null)
            {
                var user = _context.Users.FirstOrDefault(u => u.Id == team.UIUXDeveloperId);
                assignees.Add(new SelectListItem { Value = team.UIUXDeveloperId, Text = user.Email });
            }
            if (team.FrontendDeveloperId != null)
            {
                var user = _context.Users.FirstOrDefault(u => u.Id == team.FrontendDeveloperId);
                assignees.Add(new SelectListItem { Value = team.FrontendDeveloperId, Text = user.Email });
            }
            if (team.BackendDeveloperId != null)
            {
                var user = _context.Users.FirstOrDefault(u => u.Id == team.BackendDeveloperId);
                assignees.Add(new SelectListItem { Value = team.BackendDeveloperId, Text = user.Email });
            }
            if (team.TesterId != null)
            {
                var user = _context.Users.FirstOrDefault(u => u.Id == team.TesterId);
                assignees.Add(new SelectListItem { Value = team.TesterId, Text = user.Email });
            }
            if (team.TeamLeadId != null)
            {
                var user = _context.Users.FirstOrDefault(u => u.Id == team.TeamLeadId);
                assignees.Add(new SelectListItem { Value = team.TeamLeadId, Text = user.Email });
            }

            return Json(assignees);
        }



        // POST: Tasks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,AssigneeId,ManagerId,Status,CreatedAt,DueDate,TeamId")] Tasks tasks)
        {
           
                _context.Add(tasks);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
           
            //return View(tasks);
        }

        // GET: Tasks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (id == null || _context.Tasks == null)
            {
                return NotFound();
            }

            var tasks = await _context.Tasks.FindAsync(id);
            if (tasks == null)
            {
                return NotFound();
            }
            ViewData["AssigneeId"] = new SelectList(_context.Users, "Id", "Email", tasks.AssigneeId);
            ViewData["ManagerId"] = new SelectList(_context.Users, "Id", "Email", tasks.ManagerId);
            ViewData["TeamId"] = new SelectList(_context.Teams.Where(t => t.ReportsToId == currentUser.Id), "Id", "Name");

            return View(tasks);
        }

        // POST: Tasks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,AssigneeId,ManagerId,Status,CreatedAt,DueDate,TeamId")] Tasks tasks)
        {
            Console.WriteLine("Called");
            if (id != tasks.Id)
            {
                return NotFound();
            }

                Console.WriteLine("Edit error");    
                try
                {
                    _context.Update(tasks);
                    await _context.SaveChangesAsync();
                    Console.WriteLine("Edit saved");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TasksExists(tasks.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                ViewData["AssigneeId"] = new SelectList(_context.Users, "Id", "Email", tasks.AssigneeId);
                ViewData["ManagerId"] = new SelectList(_context.Users, "Id", "Email", tasks.ManagerId);
                ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Name", tasks.TeamId);
                return RedirectToAction(nameof(Index));
           
        }

        // GET: Tasks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Tasks == null)
            {
                return NotFound();
            }

            var tasks = await _context.Tasks
                .Include(t => t.Assignee)
                .Include(t => t.Manager)
                .Include(t => t.Team)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tasks == null)
            {
                return NotFound();
            }

            return View(tasks);
        }

        // POST: Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Tasks == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Tasks'  is null.");
            }
            var tasks = await _context.Tasks.FindAsync(id);
            if (tasks != null)
            {
                _context.Tasks.Remove(tasks);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TasksExists(int id)
        {
          return (_context.Tasks?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
