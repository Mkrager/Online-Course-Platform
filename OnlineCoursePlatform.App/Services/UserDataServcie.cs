using OnlineCoursePlatform.App.Contracts;
using OnlineCoursePlatform.App.ViewModels.User;
using System.Net.Http.Headers;
using System.Text.Json;

namespace OnlineCoursePlatform.App.Services
{
    public class UserDataServcie : IUserDataService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;
        private readonly IAuthenticationService _authenticationService;

        public UserDataServcie(HttpClient httpClient, IAuthenticationService authenticationService)
        {
            _httpClient = httpClient;
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            _authenticationService = authenticationService;
        }

        public async Task<UserDetailsResponse> GetDefaultUserDetailsAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://localhost:7275/api/user/default");

            var accessToken = _authenticationService.GetAccessToken();

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await _httpClient.SendAsync(request);

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
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://localhost:7275/api/user/teacher");

            var accessToken = _authenticationService.GetAccessToken();

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await _httpClient.SendAsync(request);

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
