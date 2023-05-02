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
        public async Task<IActionResult> Dashboard()
        {
            var currentUser = _userManager.GetUserAsync(User).Result;

            // get tasks assigned to the user due within the next 5 days
            var tasks = await _context.Tasks.Where(t => (t.AssigneeId == currentUser.Id && t.DueDate <= DateTime.Now.AddDays(5))||(t.ManagerId == currentUser.Id && t.DueDate <= DateTime.Now.AddDays(5))).ToListAsync();

            // get meetings scheduled for the team the user is a member of within the next 5 days
            var currentTeam = await _context.Teams
           .FirstOrDefaultAsync(t => t.UIUXDeveloperId == currentUser.Id || t.FrontendDeveloperId == currentUser.Id || t.BackendDeveloperId == currentUser.Id || t.TesterId == currentUser.Id || t.TeamLeadId == currentUser.Id || t.ReportsToId == currentUser.Id);

            var meetings = await _context.Meetings.Where(m => m.TeamId == currentTeam.Id && m.Date <= DateTime.Now.AddDays(5)).ToListAsync();

            // create a view model with the tasks and meetings
            var model = new DashboardViewModel
            {
                Tasks = tasks,
                Meetings = meetings
            };

            return View(model);
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
 