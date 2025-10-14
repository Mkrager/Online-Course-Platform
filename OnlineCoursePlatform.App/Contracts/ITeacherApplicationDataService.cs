using OnlineCoursePlatform.App.Infrastructure.Api;
using OnlineCoursePlatform.App.ViewModels.Request;
using OnlineCoursePlatform.App.ViewModels.TeacherApplication;

namespace OnlineCoursePlatform.App.Contracts
{
    public interface ITeacherApplicationDataService
    {
        Task<ApiResponse<List<TeacherApplicationListViewModel>>> GetTeacherRequests();
        Task<ApiResponse<List<TeacherApplicationListViewModel>>> GetUserPendingTeacherRequests();
        Task<ApiResponse> ApproveTeacherApplication(Guid id);
        Task<ApiResponse> CancelTeacherApplication(Guid id);
        Task<ApiResponse> RejectTeacherApplication(RejectRequestDto rejectCourseRequestDto);
        Task<ApiResponse<Guid>> CreateTeacherApplication(CreateTeacherApplicationRequest createTeacherApplicationRequest);
    }
}
