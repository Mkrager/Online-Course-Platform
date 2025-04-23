using OnlineCoursePlatform.App.ViewModels;

namespace OnlineCoursePlatform.App.Contracts
{
    public interface ICourseDataService
    {
        Task<CourseDetailViewModel> GetCourseById(Guid id);
        Task<List<CourseListViewModel>> GetAllCourses();
    }
}

