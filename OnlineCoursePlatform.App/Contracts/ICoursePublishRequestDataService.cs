using OnlineCoursePlatform.App.Infrastructure.Api;
using OnlineCoursePlatform.App.ViewModels.CoursePublishRequest;
using OnlineCoursePlatform.App.ViewModels.Request;

namespace OnlineCoursePlatform.App.Contracts
{
    public interface ICoursePublishRequestDataService
    {
        Task<ApiResponse> ApproveCourseRequest(Guid id);
        Task<ApiResponse> CancelCourseRequest(Guid id);
        Task<ApiResponse> RejectCourseRequest(RejectRequestDto rejectCourseRequestDto);
        Task<ApiResponse<Guid>> CreateCourseRequest(Guid id);
        Task<ApiResponse<List<CoursePublishRequestListViewModel>>> GetAllCoursePublishRequests(RequestStatus? status);
        Task<ApiResponse<List<CoursePublishRequestListViewModel>>> GetUserCoursePublishRequests();
    }
}
