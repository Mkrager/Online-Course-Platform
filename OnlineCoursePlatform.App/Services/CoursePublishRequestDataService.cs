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
                    var responseContent = await response.Content.ReadAsStringAsync();

                    var courseRequestId = JsonSerializer.Deserialize<Guid>(responseContent);

                    return new ApiResponse<Guid>(System.Net.HttpStatusCode.OK, courseRequestId);
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

        public async Task<ApiResponse> ApproveCourseRequest(Guid id)
        {
            try
            {
                var response = await _httpClient.PutAsync($"coursepublishrequest/approve/{id}", null);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();

                    var courseId = JsonSerializer.Deserialize<Guid>(responseContent);

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
                    var responseContent = await response.Content.ReadAsStringAsync();

                    var courseId = JsonSerializer.Deserialize<Guid>(responseContent);

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
        public async Task<ApiResponse> CancelCourseRequest(Guid id)
        {
            try
            {
                var response = await _httpClient.PutAsync($"coursepublishrequest/cancel/{id}", null);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();

                    var courseId = JsonSerializer.Deserialize<Guid>(responseContent);

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
                var responseContent = await response.Content.ReadAsStringAsync();
                var courseRequestList = JsonSerializer.Deserialize<List<CoursePublishRequestListViewModel>>(responseContent, _jsonOptions);
                return courseRequestList ?? new List<CoursePublishRequestListViewModel>();
            }

            return new List<CoursePublishRequestListViewModel>();
        }

        public async Task<List<CoursePublishRequestListViewModel>> GetUserCoursePublishRequests()
        {
            var response = await _httpClient.GetAsync("coursePublishRequest/user");

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
