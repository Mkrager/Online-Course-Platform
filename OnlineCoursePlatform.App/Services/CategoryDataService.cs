using OnlineCoursePlatform.App.Contracts;
using OnlineCoursePlatform.App.Infrastructure.Api;
using OnlineCoursePlatform.App.Infrastructure.BaseServices;
using OnlineCoursePlatform.App.ViewModels.Category;

namespace OnlineCoursePlatform.App.Services
{
    public class CategoryDataService : BaseDataService, ICategoryDataService
    {
        public CategoryDataService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
        }

        public async Task<ApiResponse<List<CategoryViewModel>>> GetAllCategories()
        {
            var response = await _httpClient.GetAsync($"category");
            return await HandleResponse<List<CategoryViewModel>>(response);
        }
    }
}
