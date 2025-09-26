using OnlineCoursePlatform.App.Infrastructure.Api;
using OnlineCoursePlatform.App.ViewModels.Course;

namespace OnlineCoursePlatform.App.Contracts
{
    public interface ICourseDataService
    {
        Task<ApiResponse<CourseDetailViewModel>> GetCourseById(Guid id);
        Task<ApiResponse<List<CourseListViewModel>>> GetAllCourses();
        Task<ApiResponse<List<CourseListViewModel>>> GetPublishedCourses();
        Task<ApiResponse<List<CourseListViewModel>>> GetCoursesByCategoryId(Guid categoryId);
        Task<ApiResponse<Guid>> CreateCourse(CourseDetailViewModel courseDetailViewModel);
        Task<ApiResponse> UpdateCourse(CourseDetailViewModel courseDetailViewModel);
        Task<ApiResponse> DeleteCourse(Guid id);
        Task<ApiResponse> UnPublish(Guid id);
    }
}