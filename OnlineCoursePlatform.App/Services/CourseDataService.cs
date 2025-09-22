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

        public async Task<CourseDetailViewModel> GetCourseById(Guid id)
        {
            var response = await _httpClient.GetAsync($"course/{id}");

            if (response.IsSuccessStatusCode)
            {
                var courseDetails = await DeserializeResponse<CourseDetailViewModel>(response);
                return courseDetails;
            }

            return new CourseDetailViewModel();
        }

        public async Task<List<CourseListViewModel>> GetAllCourses()
        {
            var response = await _httpClient.GetAsync("course");

            if (response.IsSuccessStatusCode)
            {
                var courseList = await DeserializeResponse<List<CourseListViewModel>>(response);
                return courseList;
            }

            return new List<CourseListViewModel>();
        }
        public async Task<List<CourseListViewModel>> GetPublishedCourses()
        {
            var response = await _httpClient.GetAsync("course/published");

            if (response.IsSuccessStatusCode)
            {
                var courseList = await DeserializeResponse<List<CourseListViewModel>>(response);
                return courseList;
            }

            return new List<CourseListViewModel>();
        }

        public async Task<ApiResponse<Guid>> CreateCourse(CourseDetailViewModel courseDetailViewModel)
        {
            try
            {
                var content = new StringContent(
                    JsonSerializer.Serialize(courseDetailViewModel),
                    Encoding.UTF8,
                    "application/json");

                var response = await _httpClient.PostAsync("course", content);

                if (response.IsSuccessStatusCode)
                {
                    var courseId = await DeserializeResponse<Guid>(response);
                    return new ApiResponse<Guid>(System.Net.HttpStatusCode.OK, courseId);
                }

                var errorMessages = await DeserializeResponse<List<string>>(response);
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
                var content = new StringContent(
                    JsonSerializer.Serialize(courseDetailViewModel),
                    Encoding.UTF8,
                    "application/json");

                var response = await _httpClient.PutAsync("course", content);

                if (response.IsSuccessStatusCode)
                {
                    return new ApiResponse(System.Net.HttpStatusCode.OK);
                }

                var errorMessages = await DeserializeResponse<List<string>>(response);
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
                var response = await _httpClient.DeleteAsync($"course/{id}");

                if (response.IsSuccessStatusCode)
                {
                    return new ApiResponse(System.Net.HttpStatusCode.OK);
                }

                var errorMessages = await DeserializeResponse<List<string>>(response);
                return new ApiResponse(System.Net.HttpStatusCode.BadRequest, errorMessages.FirstOrDefault());
            }
            catch (Exception ex)
            {
                return new ApiResponse(System.Net.HttpStatusCode.BadRequest, ex.Message);
            }
        }

        public async Task<List<CourseListViewModel>> GetCoursesByCategoryId(Guid categoryId)
        {
            var response = await _httpClient.GetAsync($"course/by-category/{categoryId}");

            if (response.IsSuccessStatusCode)
            {
                var courseList = await DeserializeResponse<List<CourseListViewModel>>(response);
                return courseList;
            }

            return new List<CourseListViewModel>();
        }

        public async Task<ApiResponse> UnPublish(Guid id)
        {
            var response = await _httpClient.PatchAsync($"course/{id}/unpublish", null);

            if (response.IsSuccessStatusCode)
            {
                return new ApiResponse(System.Net.HttpStatusCode.OK);
            }

            return new ApiResponse(System.Net.HttpStatusCode.NoContent);
        }

    }
}