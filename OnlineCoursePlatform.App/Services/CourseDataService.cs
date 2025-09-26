using OnlineCoursePlatform.App.Contracts;
using OnlineCoursePlatform.App.Infrastructure.Api;
using OnlineCoursePlatform.App.Infrastructure.BaseServices;
using OnlineCoursePlatform.App.ViewModels.Course;
using System.Text;
using System.Text.Json;

namespace OnlineCoursePlatform.App.Services
{
    public class CourseDataService : BaseDataService, ICourseDataService
    {
        public CourseDataService(IHttpClientFactory httpClientFactory, IAuthenticationService authenticationService) : base(httpClientFactory)
        {
        }

        public async Task<ApiResponse<CourseDetailViewModel>> GetCourseById(Guid id)
        {
            var response = await _httpClient.GetAsync($"course/{id}");
            return await HandleResponse<CourseDetailViewModel>(response);
        }
        public async Task<ApiResponse<List<CourseListViewModel>>> GetAllCourses()
        {
            var response = await _httpClient.GetAsync("course");
            return await HandleResponse<List<CourseListViewModel>>(response);
        }
        public async Task<ApiResponse<List<CourseListViewModel>>> GetPublishedCourses()
        {
            var response = await _httpClient.GetAsync("course/published");
            return await HandleResponse<List<CourseListViewModel>>(response);
        }

        public async Task<ApiResponse<Guid>> CreateCourse(CourseDetailViewModel courseDetailViewModel)
        {

            var content = new StringContent(
                JsonSerializer.Serialize(courseDetailViewModel),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PostAsync("course", content);
            return await HandleResponse<Guid>(response);
        }

        public async Task<ApiResponse> UpdateCourse(CourseDetailViewModel courseDetailViewModel)
        {
            var content = new StringContent(
                JsonSerializer.Serialize(courseDetailViewModel),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PutAsync("course", content);
            return await HandleResponse(response);
        }

        public async Task<ApiResponse> DeleteCourse(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"course/{id}");
            return await HandleResponse(response);
        }

        public async Task<ApiResponse<List<CourseListViewModel>>> GetCoursesByCategoryId(Guid categoryId)
        {
            var response = await _httpClient.GetAsync($"course/by-category/{categoryId}");
            return await HandleResponse<List<CourseListViewModel>>(response);
        }

        public async Task<ApiResponse> UnPublish(Guid id)
        {
            var response = await _httpClient.PatchAsync($"course/{id}/unpublish", null);
            return await HandleResponse(response);
        }
    }
}