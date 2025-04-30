using OnlineCoursePlatform.App.Services;
using OnlineCoursePlatform.App.ViewModels.Course;

namespace OnlineCoursePlatform.App.Contracts
{
    public interface ICourseDataService
    {
        Task<CourseDetailViewModel> GetCourseById(Guid id);
        Task<List<CourseListViewModel>> GetAllCourses();
        Task<ApiResponse<Guid>> CreateCourse(CourseDetailViewModel courseDetailViewModel);
        Task<ApiResponse<Guid>> UpdateCourse(CourseDetailViewModel courseDetailViewModel);
        Task<ApiResponse<Guid>> DeleteCourse(Guid id);
    }
}

