using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Contracts.Persistance
{
    public interface ICourseRepository : IAsyncRepository<Course>
    {
        Task<List<Course>> GetCoursesByUserIdAsync(string userId);
        Task<List<Course>> GetCoursesByCategoryIdAsync(Guid categoryId);
        Task<List<Course>> GetCoursesWithCategoryAndLevelAsync();
    }
}
