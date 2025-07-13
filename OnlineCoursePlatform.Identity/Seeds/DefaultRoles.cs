using Microsoft.AspNetCore.Identity;
using OnlineCoursePlatform.Application.Constants;
using OnlineCoursePlatform.Identity.Models;

namespace OnlineCoursePlatform.Identity.Seeds
{
    public static class DefaultRoles
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (!await roleManager.RoleExistsAsync(Roles.Admin))
                await roleManager.CreateAsync(new IdentityRole(Roles.Admin));

            if (!await roleManager.RoleExistsAsync(Roles.Default))
                await roleManager.CreateAsync(new IdentityRole(Roles.Default));

            if (!await roleManager.RoleExistsAsync(Roles.Moderator))
                await roleManager.CreateAsync(new IdentityRole(Roles.Moderator));

            if (!await roleManager.RoleExistsAsync(Roles.Teacher))
                await roleManager.CreateAsync(new IdentityRole(Roles.Teacher));
        }
    }
}
