using OnlineCoursePlatform.App.ViewModels.CoursePublishRequest;

namespace OnlineCoursePlatform.App.Contracts
{
    public interface ICoursePublishRequestDataService
    {
        Task<List<CoursePublishRequestListViewModel>> GetAllCoursePublishRequests();
    }
}
