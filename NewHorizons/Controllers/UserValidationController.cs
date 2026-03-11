using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NewHorizons.Models;

namespace NewHorizons.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserValidationController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserValidationController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        // GET: /api/UserValidation/CheckDisplayName?displayName=Alice
        [HttpGet("CheckDisplayName")]
        public async Task<IActionResult> CheckDisplayName(string displayName)
        {
            if (string.IsNullOrWhiteSpace(displayName))
                return BadRequest(new { error = "Display name is required" });

            var exists = await _userManager.Users.AnyAsync(u => u.DisplayName == displayName);
            return Ok(new { isTaken = exists });
        }
    }
}