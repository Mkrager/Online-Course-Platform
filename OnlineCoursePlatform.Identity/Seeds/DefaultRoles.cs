using Microsoft.AspNetCore.Identity;
using OnlineCoursePlatform.Application.Constants;
using OnlineCoursePlatform.Identity.Models;

namespace OnlineCoursePlatform.Identity.Seeds
{
    public static class DefaultRoles
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole(Roles.Admin));
            await roleManager.CreateAsync(new IdentityRole(Roles.Default));
            await roleManager.CreateAsync(new IdentityRole(Roles.Moderator));
            await roleManager.CreateAsync(new IdentityRole(Roles.Teacher));
        }
    }
}
