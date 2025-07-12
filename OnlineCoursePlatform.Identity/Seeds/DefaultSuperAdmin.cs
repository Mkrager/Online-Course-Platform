using Microsoft.AspNetCore.Identity;
using OnlineCoursePlatform.Application.Constants;
using OnlineCoursePlatform.Identity.Models;

namespace OnlineCoursePlatform.Identity.Seeds
{
    public static class DefaultSuperAdmin
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var defaultUser = new ApplicationUser
            {
                UserName = "superadmin",
                Email = "superadmin@gmail.com",
                FirstName = "Jack",
                LastName = "Shepard",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "Pa$$word123!");
                    await userManager.AddToRoleAsync(defaultUser, Roles.Default);
                    await userManager.AddToRoleAsync(defaultUser, Roles.Moderator);
                    await userManager.AddToRoleAsync(defaultUser, Roles.Admin);
                    await userManager.AddToRoleAsync(defaultUser, Roles.Teacher);
                }   
            }
        }
    }
}
