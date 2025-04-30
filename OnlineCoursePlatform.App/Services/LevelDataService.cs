using OnlineCoursePlatform.App.Contracts;
using OnlineCoursePlatform.App.ViewModels.Category;
using OnlineCoursePlatform.App.ViewModels.Level;
using System.Text.Json;

namespace OnlineCoursePlatform.App.Services
{
    public class LevelDataService : ILevelDataService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public LevelDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;

            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }
        public async Task<List<LevelViewModel>> GetAllLevels()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://localhost:7275/api/level");

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                var levelList = JsonSerializer.Deserialize<List<LevelViewModel>>(responseContent, _jsonOptions);

                return levelList;
            }

            return new List<LevelViewModel>();

        }
    }
}
