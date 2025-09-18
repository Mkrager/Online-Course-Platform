using OnlineCoursePlatform.App.Contracts;
using OnlineCoursePlatform.App.Infrastructure.BaseServices;
using OnlineCoursePlatform.App.ViewModels.Level;
using System.Text.Json;

namespace OnlineCoursePlatform.App.Services
{
    public class LevelDataService : BaseDataService, ILevelDataService
    {
        public LevelDataService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
        }

        public async Task<List<LevelViewModel>> GetAllLevels()
        {
            var response = await _httpClient.GetAsync("level");

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