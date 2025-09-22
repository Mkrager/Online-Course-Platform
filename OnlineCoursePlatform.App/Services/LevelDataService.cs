using OnlineCoursePlatform.App.Contracts;
using OnlineCoursePlatform.App.Infrastructure.BaseServices;
using OnlineCoursePlatform.App.ViewModels.Level;

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
                var levelList = await DeserializeResponse<List<LevelViewModel>>(response);
                return levelList;
            }

            return new List<LevelViewModel>();

        }
    }
}