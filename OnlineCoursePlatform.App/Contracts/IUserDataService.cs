using OnlineCoursePlatform.App.ViewModels;

namespace OnlineCoursePlatform.App.Contracts
{
    public interface IUserDataService
    {
        Task<UserDetailsResponse> GetUserDetails(string userId);
    }
}
