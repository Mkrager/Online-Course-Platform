using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Contracts.Persistance
{
    public interface ICourseRepository : IAsyncRepository<Course>
    {
        Task<bool> IsUserCourseTeacherAsync(string userId, Guid courseId);
        Task<List<Course>> GetCoursesByUserIdAsync(string userId);
        Task<Course?> GetCourseByIdWithCategoryAndLevelAsync(Guid id);
        Task<List<Course>> GetCoursesByCategoryIdAsync(Guid categoryId);
        Task<List<Course>> GetCoursesWithCategoryAndLevelAsync(bool onlyPublished = false);
        Task UpdateIsPublishedAsync(Course course, bool isPublished);
    }
}