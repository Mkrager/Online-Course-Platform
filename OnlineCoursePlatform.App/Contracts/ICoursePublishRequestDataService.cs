using OnlineCoursePlatform.App.Services;
using OnlineCoursePlatform.App.ViewModels.CoursePublishRequest;

namespace OnlineCoursePlatform.App.Contracts
{
    public interface ICoursePublishRequestDataService
    {
        Task<ApiResponse<Guid>> CreateCourseRequest(CoursePublishRequestListViewModel coursePublishRequestViewModel);
        Task<List<CoursePublishRequestListViewModel>> GetAllCoursePublishRequests();
    }
}
