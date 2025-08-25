using OnlineCoursePlatform.App.Contracts;
using OnlineCoursePlatform.App.ViewModels.Course;
using OnlineCoursePlatform.App.ViewModels.CoursePublishRequest;
using System.Text.Json;

namespace OnlineCoursePlatform.App.Services
{
    public class CoursePublishRequestDataService : ICoursePublishRequestDataService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;
        private readonly IAuthenticationService _authenticationService;

        public CoursePublishRequestDataService(HttpClient httpClient, IAuthenticationService authenticationService)
        {
            _httpClient = httpClient;
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            _authenticationService = authenticationService;
        }

        public async Task<List<CoursePublishRequestListViewModel>> GetAllCoursePublishRequests()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://localhost:7275/api/coursePublishRequest");

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                var courseRequestList = JsonSerializer.Deserialize<List<CoursePublishRequestListViewModel>>(responseContent, _jsonOptions);

                return courseRequestList;
            }

            return new List<CoursePublishRequestListViewModel>();
        }
    }
}
