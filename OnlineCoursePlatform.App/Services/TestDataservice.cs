using OnlineCoursePlatform.App.Contracts;
using OnlineCoursePlatform.App.ViewModels.Test;
using System.Text;
using System.Text.Json;

namespace OnlineCoursePlatform.App.Services
{
    public class TestDataservice : ITestDataService
    {
        private readonly HttpClient _httpClient;

        public TestDataservice(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ApiResponse<Guid>> CreateTest(TestViewModel testViewModel)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, $"https://localhost:7275/api/test")
                {
                    Content = new StringContent(JsonSerializer.Serialize(testViewModel), Encoding.UTF8, "application/json")
                };

                var response = await _httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();

                    var testId = JsonSerializer.Deserialize<Guid>(responseContent);

                    return new ApiResponse<Guid>(System.Net.HttpStatusCode.OK, testId);
                }

                var errorContent = await response.Content.ReadAsStringAsync();

                var problemDetails = JsonSerializer.Deserialize<ValidationProblemDetails>(errorContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                var allErrors = problemDetails?.Errors
                    .SelectMany(e => e.Value)
                    .ToList();

                return new ApiResponse<Guid>(System.Net.HttpStatusCode.BadRequest, Guid.Empty, allErrors.FirstOrDefault());
            }
            catch (Exception ex)
            {
                return new ApiResponse<Guid>(System.Net.HttpStatusCode.BadRequest, Guid.Empty, ex.Message);
            }
        }

        public async Task<ApiResponse> DeleteTest(Guid id)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Delete, $"https://localhost:7275/api/test/{id}");

                var response = await _httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    return new ApiResponse(System.Net.HttpStatusCode.OK);
                }

                var errorContent = await response.Content.ReadAsStringAsync();
                var errorMessage = JsonSerializer.Deserialize<List<string>>(errorContent);
                return new ApiResponse(System.Net.HttpStatusCode.BadRequest, errorMessage.FirstOrDefault());
            }
            catch (Exception ex)
            {
                return new ApiResponse(System.Net.HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}