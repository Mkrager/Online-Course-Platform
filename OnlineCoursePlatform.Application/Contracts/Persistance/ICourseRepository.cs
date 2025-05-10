using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Contracts.Persistance
{
    public interface ICourseRepository : IAsyncRepository<Course>
    {
        Task<List<Course>> GetCoursesByUserId(string userId);
        Task<List<Course>> GetCoursesByCategoryId(Guid categoryId);
        Task<List<Course>> GetAllWithCategoryAndLevel();
    }
}
