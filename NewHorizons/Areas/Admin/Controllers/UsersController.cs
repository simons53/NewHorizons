using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewHorizons.Models;

namespace NewHorizons.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var users = _userManager.Users.ToList();

            // For each user, check if they are an admin
            var userRoles = new Dictionary<string, bool>();
            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userRoles[user.Id] = roles.Contains("Admin");
            }

            ViewBag.UserRoles = userRoles; // pass to view
            return View(users);
        }

        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
                return NotFound();

            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return NotFound();

            var roles = await _userManager.GetRolesAsync(user);

            ViewBag.Roles = roles;

            return View(user);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();

            var roles = await _userManager.GetRolesAsync(user);
            ViewBag.Roles = roles;

            return View(user); // will render Delete.cshtml confirmation page
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();

            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = $"User {user.DisplayName} was deleted successfully.";
                return RedirectToAction("Index"); // back to Users list
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(user);
            }
        }

        [HttpPost]
        public async Task<IActionResult> ToggleAdmin(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();

            var roles = await _userManager.GetRolesAsync(user);

            if (roles.Contains("Admin"))
            {
                // Remove Admin, ensure User role exists
                await _userManager.RemoveFromRoleAsync(user, "Admin");
                if (!roles.Contains("User"))
                {
                    await _userManager.AddToRoleAsync(user, "User");
                }
            }
            else
            {
                // Add Admin, remove User role
                await _userManager.AddToRoleAsync(user, "Admin");
                if (roles.Contains("User"))
                {
                    await _userManager.RemoveFromRoleAsync(user, "User");
                }
            }

            return RedirectToAction("Details", new { id = user.Id });
        }
    }
}