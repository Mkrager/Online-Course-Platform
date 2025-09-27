using OnlineCoursePlatform.App.Infrastructure.Api;
using OnlineCoursePlatform.App.ViewModels.User;

namespace OnlineCoursePlatform.App.Contracts
{
    public interface IUserDataService
    {
        Task<ApiResponse<UserDetailsResponse>> GetTeacherDetailsAsync();
        Task<ApiResponse<UserDetailsResponse>> GetDefaultUserDetailsAsync();
    }
}
