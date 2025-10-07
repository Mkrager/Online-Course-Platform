using OnlineCoursePlatform.App.Contracts;
using OnlineCoursePlatform.App.Infrastructure.Api;
using OnlineCoursePlatform.App.Infrastructure.BaseServices;
using OnlineCoursePlatform.App.ViewModels.Request;
using OnlineCoursePlatform.App.ViewModels.TeacherApplication;
using System.Text.Json;
using System.Text;

namespace OnlineCoursePlatform.App.Services
{
    public class TeacherApplicationDataService : BaseDataService, ITeacherApplicationDataService
    {
        public TeacherApplicationDataService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
        }

        public async Task<ApiResponse> ApproveTeacherApplication(Guid id)
        {
            var response = await _httpClient.PutAsync($"teacherApplication/approve/{id}", null);
            return await HandleResponse(response);
        }

        public async Task<ApiResponse> CancelTeacherApplication(Guid id)
        {
            var response = await _httpClient.PutAsync($"teacherApplication/cancel/{id}", null);
            return await HandleResponse(response);
        }

        public async Task<ApiResponse<Guid>> CreateTeacherApplication(CreateTeacherApplicationRequest createTeacherApplicationRequest)
        {
            var content = new StringContent(
                JsonSerializer.Serialize(createTeacherApplicationRequest),
                Encoding.UTF8,
                "application/json");


            var response = await _httpClient.PostAsync($"teacherApplication", content);
            return await HandleResponse<Guid>(response);
        }

        public async Task<ApiResponse> RejectTeacherApplication(RejectRequestDto rejectCourseRequestDto)
        {
            var content = new StringContent(
                JsonSerializer.Serialize(rejectCourseRequestDto),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PutAsync($"teacherApplication/reject", content);
            return await HandleResponse(response);
        }
    }
}