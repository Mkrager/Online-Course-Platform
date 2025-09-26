using OnlineCoursePlatform.App.Infrastructure.Api;
using OnlineCoursePlatform.App.ViewModels.Category;

namespace OnlineCoursePlatform.App.Contracts
{
    public interface ICategoryDataService
    {
        Task<ApiResponse<List<CategoryViewModel>>> GetAllCategories();
    }
}
