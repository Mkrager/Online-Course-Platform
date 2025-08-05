using OnlineCoursePlatform.Application.DTOs.User;

namespace OnlineCoursePlatform.Application.Contracts.Identity
{
    public interface IUserService
    {
        Task<UserDetailsResponse> GetUserDetailsAsync(string userId);
        Task AssignRoleAsync(string userId, string roleName);
    }
}
