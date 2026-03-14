using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using NewHorizons.Models;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NewHorizons.Services
{
    public class ApplicationUserClaimsPrincipalFactory
        : UserClaimsPrincipalFactory<ApplicationUser, IdentityRole>
    {
        public ApplicationUserClaimsPrincipalFactory(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IOptions<IdentityOptions> optionsAccessor)
            : base(userManager, roleManager, optionsAccessor)
        { }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
        {
            var identity = await base.GenerateClaimsAsync(user);

            // Add the DisplayName claim
            identity.AddClaim(new Claim("DisplayName", user.DisplayName ?? ""));

            return identity;
        }
    }
}