using Microsoft.AspNetCore.Diagnostics;
using OnlineCoursePlatform.App.Contracts;
using OnlineCoursePlatform.App.ViewModels.Course;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace OnlineCoursePlatform.App.Services
{
    public class CourseDataService : ICourseDataService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;
        private readonly IAuthenticationService _authenticationService;

        public CourseDataService(HttpClient httpClient, IAuthenticationService authenticationService)
        {
            _httpClient = httpClient;
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            _authenticationService = authenticationService;
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

        public async Task<ApiResponse<Guid>> CreateCourse(CourseDetailViewModel courseDetailViewModel)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, $"https://localhost:7275/api/course")
                {
                    Content = new StringContent(JsonSerializer.Serialize(courseDetailViewModel), Encoding.UTF8, "application/json")
                };

                string accessToken = _authenticationService.GetAccessToken();

                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                var response = await _httpClient.SendAsync(request);


                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();

                    var courseId = JsonSerializer.Deserialize<Guid>(responseContent);

                    return new ApiResponse<Guid>(System.Net.HttpStatusCode.OK, courseId);
                }

                var errorContent = await response.Content.ReadAsStringAsync();
                var errorMessages = JsonSerializer.Deserialize<List<string>>(errorContent);
                return new ApiResponse<Guid>(System.Net.HttpStatusCode.BadRequest, Guid.Empty, errorMessages.FirstOrDefault());
            }
            catch (Exception ex)
            {
                return new ApiResponse<Guid>(System.Net.HttpStatusCode.BadRequest, Guid.Empty, ex.Message);
            }
        }

        public async Task<ApiResponse> UpdateCourse(CourseDetailViewModel courseDetailViewModel)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Put, $"https://localhost:7275/api/course")
                {
                    Content = new StringContent(JsonSerializer.Serialize(courseDetailViewModel), Encoding.UTF8, "application/json")
                };

                var response = await _httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    return new ApiResponse(System.Net.HttpStatusCode.OK);
                }

                var errorContent = await response.Content.ReadAsStringAsync();
                var errorMessages = JsonSerializer.Deserialize<List<string>>(errorContent);
                return new ApiResponse(System.Net.HttpStatusCode.BadRequest, errorMessages.FirstOrDefault());
            }
            catch (Exception ex)
            {
                return new ApiResponse(System.Net.HttpStatusCode.BadRequest, ex.Message);
            }
        }

        public async Task<ApiResponse> DeleteCourse(Guid id)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Delete, $"https://localhost:7275/api/course/{id}");

                var response = await _httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    return new ApiResponse(System.Net.HttpStatusCode.OK);
                }

                var errorContent = await response.Content.ReadAsStringAsync();
                var errorMessages = JsonSerializer.Deserialize<List<string>>(errorContent);
                return new ApiResponse(System.Net.HttpStatusCode.BadRequest, errorMessages.FirstOrDefault());
            }
            catch (Exception ex)
            {
                return new ApiResponse(System.Net.HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}
