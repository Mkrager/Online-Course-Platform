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

            var content = new StringContent(
                JsonSerializer.Serialize(testViewModel),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PostAsync("test", content);
            return await HandleResponse<Guid>(response);
        }

        public async Task<ApiResponse> DeleteTest(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"test/{id}");
            return await HandleResponse(response);
        }

        public async Task<ApiResponse<TestViewModel>> GetTestById(Guid id)
        {
            var response = await _httpClient.GetAsync($"test/{id}");
            return await HandleResponse<TestViewModel>(response);
        }

        public async Task<ApiResponse<List<TestViewModel>>> GetTestByLessonId(Guid lessonId)
        {
            var response = await _httpClient.GetAsync($"test/by-lesson/{lessonId}");
            return await HandleResponse<List<TestViewModel>>(response);
        }

        public async Task<ApiResponse> UpdateTest(TestViewModel testViewModel)
        {

            var content = new StringContent(
                JsonSerializer.Serialize(testViewModel),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PutAsync("test", content);
            return await HandleResponse(response);
        }
    }
}