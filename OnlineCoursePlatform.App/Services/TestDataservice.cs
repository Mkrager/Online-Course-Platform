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
                var errorMessage = JsonSerializer.Deserialize<List<string>>(errorContent);
                return new ApiResponse<Guid>(System.Net.HttpStatusCode.BadRequest, Guid.Empty, errorMessage.FirstOrDefault());
            }
            catch (Exception ex)
            {
                return new ApiResponse<Guid>(System.Net.HttpStatusCode.BadRequest, Guid.Empty, ex.Message);
            }
        }
    }
}
