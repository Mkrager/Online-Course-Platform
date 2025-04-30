using OnlineCoursePlatform.App.Contracts;
using OnlineCoursePlatform.App.ViewModels.User;
using System.Text.Json;

namespace OnlineCoursePlatform.App.Services
{
    public class UserDataServcie : IUserDataService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public UserDataServcie(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<UserDetailsResponse> GetUserDetails(string userId)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://localhost:7275/api/User/GetUserDetails/{userId}");

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
