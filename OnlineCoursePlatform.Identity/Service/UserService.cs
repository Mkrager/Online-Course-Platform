using Microsoft.AspNetCore.Identity;
using OnlineCoursePlatform.Application.Contracts.Identity;
using OnlineCoursePlatform.Application.DTOs.User;
using OnlineCoursePlatform.Identity.Models;

namespace OnlineCoursePlatform.Identity.Service
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<UserDetailsResponse> GetUserDetails(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User not found.");
            }

            UserDetailsResponse userDetailsResponse = new UserDetailsResponse()
            {
                Email = user.Email,
                UserName = user.UserName
            };

            return userDetailsResponse;
        }
    }
}
