using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Contracts.Persistance
{
    public interface ILessonRepository : IAsyncRepository<Lesson>
    {
        Task<List<Lesson>> GetCourseLessons(Guid courseId);
    }
}
