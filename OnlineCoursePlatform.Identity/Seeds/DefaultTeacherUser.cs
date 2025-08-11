using Microsoft.AspNetCore.Identity;
using OnlineCoursePlatform.Application.Constants;
using OnlineCoursePlatform.Identity.Models;

namespace OnlineCoursePlatform.Identity.Seeds
{
    public static class DefaultTeacherUser
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var defaultUser = new ApplicationUser
            {
                UserName = "teacher",
                Email = "teacher@gmail.com",
                FirstName = "James",
                LastName = "Ford",
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
                    await userManager.AddToRoleAsync(defaultUser, Roles.Teacher);
                }
            }
        }

    }
}
