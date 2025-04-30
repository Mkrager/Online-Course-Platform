using OnlineCoursePlatform.App.Services;
using OnlineCoursePlatform.App.ViewModels;
using OnlineCoursePlatform.App.ViewModels.Authenticate;

namespace OnlineCoursePlatform.App.Contracts
{
    public interface IAuthenticationService
    {
        Task<ApiResponse<bool>> Authenticate(AuthenticateRequest request);
        Task<ApiResponse<bool>> Register(RegistrationRequest request);
        Task Logout();
        string GetAccessToken();
    }
}
