using OnlineCoursePlatform.App.Services;
using OnlineCoursePlatform.App.ViewModels.Lesson;

namespace OnlineCoursePlatform.App.Contracts
{
    public interface ILessonDataService
    {
        Task<List<LessonViewModel>> GetCourseLessons(Guid courseId);
        Task<ApiResponse<Guid>> CreateLesson(LessonViewModel lessonViewModel);
        Task<ApiResponse> DeleteLesson(Guid id);
        Task<ApiResponse> UpdateLesson(LessonViewModel lessonViewModel);
        Task<LessonViewModel> GetLessonById(Guid id);
    }
}
