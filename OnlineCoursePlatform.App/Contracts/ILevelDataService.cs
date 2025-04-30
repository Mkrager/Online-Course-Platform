using OnlineCoursePlatform.App.ViewModels.Level;

namespace OnlineCoursePlatform.App.Contracts
{
    public interface ILevelDataService
    {
        Task<List<LevelViewModel>> GetAllLevels();
    }
}
