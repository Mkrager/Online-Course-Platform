using OnlineCoursePlatform.App.Contracts;
using OnlineCoursePlatform.App.ViewModels.TestAttempt;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace OnlineCoursePlatform.App.Services
{
    public class TestAttemptDataService : ITestAttemptDataService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;
        private readonly IAuthenticationService _authenticationService;

        public TestAttemptDataService(HttpClient httpClient, IAuthenticationService authenticationService)
        {
            _httpClient = httpClient;
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            _authenticationService = authenticationService;
        }

        public Task<ApiResponse<Guid>> EndTestAttempt(EndTestAttemptViewModel endAttemptViewModel)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse<Guid>> StartTestAttempt(StartTestAttemptViewModel startAttemptViewModel)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, $"https://localhost:7275/api/TestAttempt")
                {
                    Content = new StringContent(JsonSerializer.Serialize(startAttemptViewModel), Encoding.UTF8, "application/json")
                };

                string accessToken = _authenticationService.GetAccessToken();

                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                var response = await _httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();

                    var lessonId = JsonSerializer.Deserialize<Guid>(responseContent);

                    return new ApiResponse<Guid>(System.Net.HttpStatusCode.OK, lessonId);
                }

                var errorContent = await response.Content.ReadAsStringAsync();
                var errorMessages = JsonSerializer.Deserialize<List<string>>(errorContent);
                return new ApiResponse<Guid>(System.Net.HttpStatusCode.BadRequest, Guid.Empty, errorMessages.FirstOrDefault());
            }
            catch (Exception ex)
            {
                return new ApiResponse<Guid>(System.Net.HttpStatusCode.BadRequest, Guid.Empty, ex.Message);
            }
        }
    }
}
