using OnlineCoursePlatform.Application.DTOs.User;

namespace OnlineCoursePlatform.Application.Contracts.Identity
{
    public interface IUserService
    {
        Task<UserDetailsResponse> GetUserDetails(string userId);
    }
}
