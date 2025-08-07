using OnlineCoursePlatform.App.ViewModels.User;

namespace OnlineCoursePlatform.App.Contracts
{
    public interface IUserDataService
    {
        Task<UserDetailsResponse> GetTeacherDetailsAsync();
        Task<UserDetailsResponse> GetDefaultUserDetailsAsync();
    }
}
