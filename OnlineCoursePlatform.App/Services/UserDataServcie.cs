using OnlineCoursePlatform.App.Contracts;
using OnlineCoursePlatform.App.Infrastructure.Api;
using OnlineCoursePlatform.App.Infrastructure.BaseServices;
using OnlineCoursePlatform.App.ViewModels.User;

namespace OnlineCoursePlatform.App.Services
{
    public class UserDataServcie : BaseDataService, IUserDataService
    {
        public UserDataServcie(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
        }

        public async Task<ApiResponse<UserDetailsResponse>> GetDefaultUserDetailsAsync()
        {
            var response = await _httpClient.GetAsync("user/default");
            return await HandleResponse<UserDetailsResponse>(response);
        }

        public async Task<ApiResponse<UserDetailsResponse>> GetTeacherDetailsAsync()
        {
            var response = await _httpClient.GetAsync("user/teacher");
            return await HandleResponse<UserDetailsResponse>(response);
        }
    }
}