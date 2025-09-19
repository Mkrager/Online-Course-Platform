using OnlineCoursePlatform.App.Contracts;
using OnlineCoursePlatform.App.Infrastructure.Api;
using OnlineCoursePlatform.App.Infrastructure.BaseServices;
using OnlineCoursePlatform.App.ViewModels.Test;
using System.Text;
using System.Text.Json;

namespace OnlineCoursePlatform.App.Services
{
    public class TestDataService : BaseDataService, ITestDataService
    {
        public TestDataService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
        }

        public async Task<ApiResponse<Guid>> CreateTest(TestViewModel testViewModel)
        {
            try
            {
                var content = new StringContent(
                    JsonSerializer.Serialize(testViewModel),
                    Encoding.UTF8,
                    "application/json");

                var response = await _httpClient.PostAsync("test", content);

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
                var response = await _httpClient.DeleteAsync($"test/{id}");

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
            var response = await _httpClient.GetAsync($"test/{id}");

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
            var response = await _httpClient.GetAsync($"test/by-lesson/{lessonId}");

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
                var content = new StringContent(
                    JsonSerializer.Serialize(testViewModel),
                    Encoding.UTF8,
                    "application/json");

                var response = await _httpClient.PutAsync("test", content);

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