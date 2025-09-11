using OnlineCoursePlatform.Application.DTOs.User;

namespace OnlineCoursePlatform.Application.Contracts.Identity
{
    public interface IUserService
    {
        Task<UserDetailsResponse> GetUserDetailsAsync(string userId);
        Task<Dictionary<string, string>> GetUserNamesByIdsAsync(IEnumerable<string> userIds);
        Task AssignRoleAsync(string userId, string roleName);
    }
}
