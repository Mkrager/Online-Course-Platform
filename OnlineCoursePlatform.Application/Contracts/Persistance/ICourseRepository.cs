using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Contracts.Persistance
{
    public interface ICourseRepository
    {
        Task<List<Course>> GetCoursesByUserId(string userId);
    }
}
