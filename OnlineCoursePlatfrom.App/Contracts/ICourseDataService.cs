using OnlineCoursePlatfrom.App.ViewModels;

namespace OnlineCoursePlatfrom.App.Contracts
{
    public interface ICourseDataService
    {
        Task<CourseDetailViewModel> GetCourseById(Guid id);
    }
}
