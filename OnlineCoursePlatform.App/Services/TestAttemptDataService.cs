using OnlineCoursePlatform.App.Contracts;
using OnlineCoursePlatform.App.Infrastructure.Api;
using OnlineCoursePlatform.App.Infrastructure.BaseServices;
using OnlineCoursePlatform.App.ViewModels.TestAttempt;
using System.Text;
using System.Text.Json;

namespace OnlineCoursePlatform.App.Services
{
    public class TestAttemptDataService : BaseDataService, ITestAttemptDataService
    {
        public TestAttemptDataService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
        }

        public async Task<ApiResponse> EndTestAttempt(EndTestAttemptViewModel endAttemptViewModel)
        {

            var content = new StringContent(
                JsonSerializer.Serialize(endAttemptViewModel),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PutAsync("testAttempt", content);
            return await HandleResponse(response);
        }

        public async Task<ApiResponse<Guid>> StartTestAttempt(StartTestAttemptViewModel startAttemptViewModel)
        {

            var content = new StringContent(
                JsonSerializer.Serialize(startAttemptViewModel),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PostAsync("testAttempt", content);
            return await HandleResponse<Guid>(response);
        }
    }
}