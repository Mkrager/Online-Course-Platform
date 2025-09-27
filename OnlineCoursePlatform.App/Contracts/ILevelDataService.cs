using OnlineCoursePlatform.App.Infrastructure.Api;
using OnlineCoursePlatform.App.ViewModels.Level;

namespace OnlineCoursePlatform.App.Contracts
{
    public interface ILevelDataService
    {
        Task<ApiResponse<List<LevelViewModel>>> GetAllLevels();
    }
}
