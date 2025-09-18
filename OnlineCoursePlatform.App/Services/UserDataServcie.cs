using OnlineCoursePlatform.App.Contracts;
using OnlineCoursePlatform.App.Infrastructure.BaseServices;
using OnlineCoursePlatform.App.ViewModels.User;
using System.Text.Json;

namespace OnlineCoursePlatform.App.Services
{
    public class UserDataServcie : BaseDataService, IUserDataService
    {
        public UserDataServcie(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
        }

        public async Task<UserDetailsResponse> GetDefaultUserDetailsAsync()
        {
            var response = await _httpClient.GetAsync("user/default");

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                var userDetails = JsonSerializer.Deserialize<UserDetailsResponse>(responseContent, _jsonOptions);

                return userDetails;
            }

            return new UserDetailsResponse();
        }

        public async Task<UserDetailsResponse> GetTeacherDetailsAsync()
        {
            var response = await _httpClient.GetAsync("user/teacher");

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                var userDetails = JsonSerializer.Deserialize<UserDetailsResponse>(responseContent, _jsonOptions);

                return userDetails;
            }

            return new UserDetailsResponse();
        }
    }
}
