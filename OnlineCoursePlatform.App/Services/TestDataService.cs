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
                    var testId = await DeserializeResponse<Guid>(response);
                    return new ApiResponse<Guid>(System.Net.HttpStatusCode.OK, testId);
                }

                var errorMessages = await DeserializeResponse<List<string>>(response);
                return new ApiResponse<Guid>(System.Net.HttpStatusCode.BadRequest, Guid.Empty, errorMessages.FirstOrDefault());
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

                var errorMessages = await DeserializeResponse<List<string>>(response);
                return new ApiResponse(System.Net.HttpStatusCode.BadRequest, errorMessages.FirstOrDefault());
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
                var test = await DeserializeResponse<TestViewModel>(response);
                return test;
            }

            return new TestViewModel();
        }

        public async Task<List<TestViewModel>> GetTestByLessonId(Guid lessonId)
        {
            var response = await _httpClient.GetAsync($"test/by-lesson/{lessonId}");

            if (response.IsSuccessStatusCode)
            {
                var testsList = await DeserializeResponse<List<TestViewModel>>(response);
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

                var errorMessages = await DeserializeResponse<List<string>>(response);
                return new ApiResponse(System.Net.HttpStatusCode.BadRequest, errorMessages.FirstOrDefault());
            }
            catch (Exception ex)
            {
                return new ApiResponse(System.Net.HttpStatusCode.BadRequest, ex.Message);
            }
        }        
    }
}