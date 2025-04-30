using OnlineCoursePlatform.App.Contracts;
using OnlineCoursePlatform.App.ViewModels.Category;
using OnlineCoursePlatform.App.ViewModels.Course;
using System.Text.Json;

namespace OnlineCoursePlatform.App.Services
{
    public class CategoryDataService : ICategoryDataService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public CategoryDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;

            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

        }
        public async Task<List<CategoryViewModel>> GetAllCategories()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://localhost:7275/api/category");

            var response = await _httpClient.SendAsync(request);

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
