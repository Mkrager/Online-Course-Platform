using OnlineCoursePlatform.App.Infrastructure.Api;
using OnlineCoursePlatform.App.ViewModels.Course;

namespace OnlineCoursePlatform.App.Contracts
{
    public interface ICourseDataService
    {
        Task<CourseDetailViewModel> GetCourseById(Guid id);
        Task<List<CourseListViewModel>> GetAllCourses();
        Task<List<CourseListViewModel>> GetPublishedCourses();
        Task<List<CourseListViewModel>> GetCoursesByCategoryId(Guid categoryId);
        Task<ApiResponse<Guid>> CreateCourse(CourseDetailViewModel courseDetailViewModel);
        Task<ApiResponse> UpdateCourse(CourseDetailViewModel courseDetailViewModel);
        Task<ApiResponse> DeleteCourse(Guid id);
        Task<ApiResponse> UnPublish(Guid id);
    }
}

