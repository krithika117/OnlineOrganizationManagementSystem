using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineOrganizationManagementSystem.Data;
using OnlineOrganizationManagementSystem.Models.ViewModels;

namespace OnlineOrganizationManagementSystem.Controllers
{
    public class UsermanagementController : Controller
    {
        private readonly ApplicationDbContext _context; 
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UsermanagementController(ApplicationDbContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
                _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles ="Admin")]
        [HttpGet]
        public IActionResult Create()
        {
            // Get list of roles to display in dropdown
            var roles = _roleManager.Roles.Select(r => new SelectListItem { Value = r.Name, Text = r.Name });
            ViewBag.Roles = roles;

            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(UserCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Create new user
                var user = new IdentityUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, "Password@123");

                if (result.Succeeded)
                {
                    // Add user to selected role
                    await _userManager.AddToRoleAsync(user, model.Role);

                    // Redirect to list of users
                    return RedirectToAction("Index", "Usermanagement");
                }

                // Add errors to ModelState if user creation failed
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            // If ModelState is invalid, return view with errors
            var roles = _roleManager.Roles.Select(r => new SelectListItem { Value = r.Name, Text = r.Name });
            ViewBag.Roles = roles;
            return View(model);
        }
    }
}
