using OnlineCoursePlatfrom.App.Contracts;
using OnlineCoursePlatfrom.App.ViewModels;
using System.Text.Json;

namespace OnlineCoursePlatfrom.App.Services
{
    public class CourseDataService : ICourseDataService
    {
        private readonly HttpClient _httpClient;

        public CourseDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<CourseDetailViewModel> GetCourseById(Guid id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://localhost:7275/api/course/{id}");

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                var courseDetails = JsonSerializer.Deserialize<CourseDetailViewModel>(responseContent);

                return courseDetails;
            }
            
            return new CourseDetailViewModel();
        }
    }
}
