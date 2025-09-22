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
            try
            {
                var response = await _httpClient.PostAsync($"coursepublishrequest/{courseId}", null);

                if (response.IsSuccessStatusCode)
                {
                    var courseRequestId = await DeserializeResponse<Guid>(response);
                    return new ApiResponse<Guid>(System.Net.HttpStatusCode.OK, courseRequestId);
                }

                var errorMessages = await DeserializeResponse<List<string>>(response);
                return new ApiResponse<Guid>(System.Net.HttpStatusCode.BadRequest, Guid.Empty, errorMessages.FirstOrDefault());
            }
            catch (Exception ex)
            {
                return new ApiResponse<Guid>(System.Net.HttpStatusCode.BadRequest, Guid.Empty, ex.Message);
            }
        }

        public async Task<ApiResponse> ApproveCourseRequest(Guid id)
        {
            try
            {
                var response = await _httpClient.PutAsync($"coursepublishrequest/approve/{id}", null);

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
        
        public async Task<ApiResponse> RejectCourseRequest(RejectCourseRequestDto rejectCourseRequestDto)
        {
            try
            {
                var content = new StringContent(
                    JsonSerializer.Serialize(rejectCourseRequestDto),
                    Encoding.UTF8,
                    "application/json");

                var response = await _httpClient.PutAsync($"coursepublishrequest/reject", content);

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
        public async Task<ApiResponse> CancelCourseRequest(Guid id)
        {
            try
            {
                var response = await _httpClient.PutAsync($"coursepublishrequest/cancel/{id}", null);

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

        public async Task<List<CoursePublishRequestListViewModel>> GetAllCoursePublishRequests(CoursePublishStatus? status)
        {
            var url = "coursePublishRequest";
            if (status.HasValue)
            {
                url += $"?status={status.Value}";
            }

            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var courseRequestList = await DeserializeResponse<List<CoursePublishRequestListViewModel>>(response);
                return courseRequestList;
            }

            return new List<CoursePublishRequestListViewModel>();
        }

        public async Task<List<CoursePublishRequestListViewModel>> GetUserCoursePublishRequests()
        {
            var response = await _httpClient.GetAsync("coursePublishRequest/user");

            if (response.IsSuccessStatusCode)
            {
                var courseRequestList = await DeserializeResponse<List<CoursePublishRequestListViewModel>>(response);
                return courseRequestList;
            }

            return new List<CoursePublishRequestListViewModel>();
        }

    }
}
