using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineCoursePlatform.Application.Contracts.Identity;
using OnlineCoursePlatform.Application.DTOs.User;
using OnlineCoursePlatform.Identity.Models;

namespace OnlineCoursePlatform.Identity.Service
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task AssignRoleAsync(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                throw new ArgumentNullException(nameof(user), "User not found.");

            if (!await _roleManager.RoleExistsAsync(roleName))
                throw new Exception($"Role '{roleName}' doesn't exist.");

            var alreadyInRole = await _userManager.IsInRoleAsync(user, roleName);
            if (alreadyInRole)
                throw new Exception($"User is already in role '{roleName}'.");

            var result = await _userManager.AddToRoleAsync(user, roleName);
            if (!result.Succeeded)
            {
                var errors = string.Join("; ", result.Errors.Select(e => e.Description));
                throw new Exception($"Failed to add role: {errors}");
            }
        }

        public async Task<UserDetailsResponse> GetUserDetailsAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User not found.");
            }

            var roles = await _userManager.GetRolesAsync(user);

            UserDetailsResponse userDetailsResponse = new UserDetailsResponse()
            {
                Email = user.Email,
                UserName = user.UserName,
                Roles = roles.ToList()
            };

            return userDetailsResponse;
        }

        public async Task<Dictionary<string, string>> GetUserNamesByIdsAsync(IEnumerable<string> userIds)
        {
            var users = await _userManager.Users
                .Where(u => userIds.Contains(u.Id))
                .Select(u => new { u.Id, u.UserName })
                .ToListAsync();

            return users.ToDictionary(u => u.Id, u => u.UserName);
        }
    }
}
