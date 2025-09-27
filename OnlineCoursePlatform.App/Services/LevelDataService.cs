using OnlineCoursePlatform.App.Contracts;
using OnlineCoursePlatform.App.Infrastructure.Api;
using OnlineCoursePlatform.App.Infrastructure.BaseServices;
using OnlineCoursePlatform.App.ViewModels.Level;

namespace OnlineCoursePlatform.App.Services
{
    public class LevelDataService : BaseDataService, ILevelDataService
    {
        public LevelDataService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
        }

        public async Task<ApiResponse<List<LevelViewModel>>> GetAllLevels()
        {
            var response = await _httpClient.GetAsync("level");
            return await HandleResponse<List<LevelViewModel>>(response);
        }
    }
}