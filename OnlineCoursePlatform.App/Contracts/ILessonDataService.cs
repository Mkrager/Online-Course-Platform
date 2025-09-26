using OnlineCoursePlatform.App.Infrastructure.Api;
using OnlineCoursePlatform.App.ViewModels.Lesson;

namespace OnlineCoursePlatform.App.Contracts
{
    public interface ILessonDataService
    {
        Task<ApiResponse<List<LessonViewModel>>> GetCourseLessons(Guid courseId);
        Task<ApiResponse<Guid>> CreateLesson(LessonViewModel lessonViewModel);
        Task<ApiResponse> DeleteLesson(Guid id);
        Task<ApiResponse> UpdateLesson(LessonViewModel lessonViewModel);
        Task<ApiResponse<LessonViewModel>> GetLessonById(Guid id);
    }
}
