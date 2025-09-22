using OnlineCoursePlatform.App.Contracts;
using OnlineCoursePlatform.App.Infrastructure.BaseServices;
using OnlineCoursePlatform.App.ViewModels.Category;

namespace OnlineCoursePlatform.App.Services
{
    public class CategoryDataService : BaseDataService, ICategoryDataService
    {
        public CategoryDataService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
        }

        public async Task<List<CategoryViewModel>> GetAllCategories()
        {
            var response = await _httpClient.GetAsync($"category");

            if (response.IsSuccessStatusCode)
            {
                var categoryList = await DeserializeResponse<List<CategoryViewModel>>(response);
                return categoryList;
            }

            return new List<CategoryViewModel>();
        }
    }
}
