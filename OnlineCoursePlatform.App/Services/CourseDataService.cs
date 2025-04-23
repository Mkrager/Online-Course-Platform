using OnlineCoursePlatform.App.Contracts;
using OnlineCoursePlatform.App.ViewModels;
using System.Text.Json;

namespace OnlineCoursePlatform.App.Services
{
    public class CourseDataService : ICourseDataService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public CourseDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<CourseDetailViewModel> GetCourseById(Guid id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://localhost:7275/api/course/{id}");

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                var courseDetails = JsonSerializer.Deserialize<CourseDetailViewModel>(responseContent, _jsonOptions);

                return courseDetails;
            }

            return new CourseDetailViewModel();
        }

        public async Task<List<CourseListViewModel>> GetAllCourses()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://localhost:7275/api/course");

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                var courseList = JsonSerializer.Deserialize<List<CourseListViewModel>>(responseContent, _jsonOptions);

                return courseList;
            }

            return new List<CourseListViewModel>();
        }
    }
}
