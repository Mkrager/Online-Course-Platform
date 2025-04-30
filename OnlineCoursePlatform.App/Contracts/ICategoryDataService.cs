using OnlineCoursePlatform.App.ViewModels.Category;

namespace OnlineCoursePlatform.App.Contracts
{
    public interface ICategoryDataService
    {
        Task<List<CategoryViewModel>> GetAllCategories();
    }
}
