using OnlineCoursePlatform.App.Contracts;
using OnlineCoursePlatform.App.Infrastructure.BaseServices;
using OnlineCoursePlatform.App.ViewModels.Category;
using System.Text.Json;

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
                var responseContent = await response.Content.ReadAsStringAsync();

                var categoryList = JsonSerializer.Deserialize<List<CategoryViewModel>>(responseContent, _jsonOptions);

                return categoryList;
            }

            return new List<CategoryViewModel>();
        }
    }
}
