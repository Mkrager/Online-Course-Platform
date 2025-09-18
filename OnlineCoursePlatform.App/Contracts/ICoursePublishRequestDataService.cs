using OnlineCoursePlatform.App.Infrastructure.Api;
using OnlineCoursePlatform.App.ViewModels.CoursePublishRequest;

namespace OnlineCoursePlatform.App.Contracts
{
    public interface ICoursePublishRequestDataService
    {
        Task<ApiResponse> ApproveCourseRequest(Guid id);
        Task<ApiResponse> CancelCourseRequest(Guid id);
        Task<ApiResponse> RejectCourseRequest(RejectCourseRequestDto rejectCourseRequestDto);
        Task<ApiResponse<Guid>> CreateCourseRequest(Guid id);
        Task<List<CoursePublishRequestListViewModel>> GetAllCoursePublishRequests(CoursePublishStatus? status);
        Task<List<CoursePublishRequestListViewModel>> GetUserCoursePublishRequests();
    }
}
