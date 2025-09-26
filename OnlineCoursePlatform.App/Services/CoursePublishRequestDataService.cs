using OnlineCoursePlatform.App.Contracts;
using OnlineCoursePlatform.App.Infrastructure.Api;
using OnlineCoursePlatform.App.Infrastructure.BaseServices;
using OnlineCoursePlatform.App.ViewModels.CoursePublishRequest;
using System.Text;
using System.Text.Json;

namespace OnlineCoursePlatform.App.Services
{
    public class CoursePublishRequestDataService : BaseDataService, ICoursePublishRequestDataService
    {
        public CoursePublishRequestDataService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
        }

        public async Task<ApiResponse<Guid>> CreateCourseRequest(Guid courseId)
        {
            var response = await _httpClient.PostAsync($"coursepublishrequest/{courseId}", null);
            return await HandleResponse<Guid>(response);
        }

        public async Task<ApiResponse> ApproveCourseRequest(Guid id)
        {
            var response = await _httpClient.PutAsync($"coursepublishrequest/approve/{id}", null);
            return await HandleResponse(response);
        }

        public async Task<ApiResponse> RejectCourseRequest(RejectCourseRequestDto rejectCourseRequestDto)
        {
            var content = new StringContent(
                JsonSerializer.Serialize(rejectCourseRequestDto),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PutAsync($"coursepublishrequest/reject", content);
            return await HandleResponse(response);
        }
        public async Task<ApiResponse> CancelCourseRequest(Guid id)
        {
            var response = await _httpClient.PutAsync($"coursepublishrequest/cancel/{id}", null);
            return await HandleResponse(response);
        }

        public async Task<ApiResponse<List<CoursePublishRequestListViewModel>>> GetAllCoursePublishRequests(CoursePublishStatus? status)
        {
            var url = "coursePublishRequest";
            if (status.HasValue)
            {
                url += $"?status={status.Value}";
            }

            var response = await _httpClient.GetAsync(url);
            return await HandleResponse<List<CoursePublishRequestListViewModel>>(response);
        }

        public async Task<ApiResponse<List<CoursePublishRequestListViewModel>>> GetUserCoursePublishRequests()
        {
            var response = await _httpClient.GetAsync("coursePublishRequest/user");
            return await HandleResponse<List<CoursePublishRequestListViewModel>>(response);
        }
    }
}
