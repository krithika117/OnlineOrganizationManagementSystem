using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineOrganizationManagementSystem.Data;
using OnlineOrganizationManagementSystem.Models;
using OnlineOrganizationManagementSystem.Models.ViewModels;
using System.Diagnostics;

namespace OnlineOrganizationManagementSystem.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }
        

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Music()
        {
            return View();
        }

        [Authorize(Roles = "User, Manager")]
        public async Task<IActionResult> Dashboard()
        {
            var currentUser = _userManager.GetUserAsync(User).Result;

            // get tasks assigned to the user due within the next 5 days
            var tasks = await _context.Tasks.Where(t => (t.AssigneeId == currentUser.Id && t.DueDate <= DateTime.Now.AddDays(5)) || (t.ManagerId == currentUser.Id && t.DueDate <= DateTime.Now.AddDays(5))).ToListAsync();

            // get meetings scheduled for the teams the user is a member of within the next 5 days
            var currentTeams = await _context.Teams
                .Where(t => t.UIUXDeveloperId == currentUser.Id || t.FrontendDeveloperId == currentUser.Id || t.BackendDeveloperId == currentUser.Id || t.TesterId == currentUser.Id || t.TeamLeadId == currentUser.Id || t.ReportsToId == currentUser.Id)
                .ToListAsync();

            if (currentTeams!=null)
            {
                var meetings = _context.Meetings
                    .Where(m => m.Date <= DateTime.Now.AddDays(5))
                    .AsEnumerable().Where(m => currentTeams.Any(t => t.Id == m.TeamId))
                    .ToList();

                var model = new DashboardViewModel
                {
                    Tasks = tasks,
                    Meetings = meetings
                };
                return View(model);
            }
            else
            {
                // create a view model with the tasks and no meetings
                var model = new DashboardViewModel
                {
                    Tasks = tasks
                };

                return View(model);
            }
        }

        public IActionResult Privacy()
            {
                return View();
            }  
          

            [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
            public IActionResult Error()
            {
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }
    }
 