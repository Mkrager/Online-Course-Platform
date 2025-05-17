using OnlineCoursePlatform.App.Contracts;
using OnlineCoursePlatform.App.ViewModels.Test;
using System.Text;
using System.Text.Json;

namespace OnlineCoursePlatform.App.Services
{
    public class TestDataService : ITestDataService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public TestDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

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

        public async Task<TestViewModel> GetTestById(Guid id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://localhost:7275/api/test/{id}");

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                var test = JsonSerializer.Deserialize<TestViewModel>(content, _jsonOptions);

                return test;
            }

            return new TestViewModel();
        }

        public async Task<List<TestViewModel>> GetTestByLessonId(Guid lessonId)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://localhost:7275/api/test/lesson/{lessonId}");

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                var testsList = JsonSerializer.Deserialize<List<TestViewModel>>(content, _jsonOptions);

                return testsList;
            }

            return new List<TestViewModel>();
        }

        public async Task<ApiResponse> UpdateTest(TestViewModel testViewModel)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Put, $"https://localhost:7275/api/lesson")
                {
                    Content = new StringContent(JsonSerializer.Serialize(testViewModel), Encoding.UTF8, "application/json")
                };

                var response = await _httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    return new ApiResponse(System.Net.HttpStatusCode.OK);
                }

                var errorContent = await response.Content.ReadAsStringAsync();
                var errrorMessage = JsonSerializer.Deserialize<List<string>>(errorContent);
                return new ApiResponse(System.Net.HttpStatusCode.BadRequest, errrorMessage.FirstOrDefault());
            }
            catch (Exception ex)
            {
                return new ApiResponse(System.Net.HttpStatusCode.BadRequest, ex.Message);
            }
        }        
    }
}