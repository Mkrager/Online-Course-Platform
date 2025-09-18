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
            try
            {
                var content = new StringContent(
                    JsonSerializer.Serialize(endAttemptViewModel),
                    Encoding.UTF8,
                    "application/json");

                var response = await _httpClient.PutAsync("testAttempt", content);

                if (response.IsSuccessStatusCode)
                {
                    return new ApiResponse(System.Net.HttpStatusCode.OK);
                }

                var errorContent = await response.Content.ReadAsStringAsync();
                var errorMessages = JsonSerializer.Deserialize<List<string>>(errorContent);
                return new ApiResponse(System.Net.HttpStatusCode.BadRequest, errorMessages.FirstOrDefault());
            }
            catch (Exception ex)
            {
                return new ApiResponse(System.Net.HttpStatusCode.BadRequest, ex.Message);
            }
        }

        public async Task<ApiResponse<Guid>> StartTestAttempt(StartTestAttemptViewModel startAttemptViewModel)
        {
            try
            {
                var content = new StringContent(
                    JsonSerializer.Serialize(startAttemptViewModel),
                    Encoding.UTF8,
                    "application/json");

                var response = await _httpClient.PostAsync("testAttempt", content);

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
