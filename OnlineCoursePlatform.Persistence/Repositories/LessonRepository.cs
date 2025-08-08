using Microsoft.EntityFrameworkCore;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Persistence.Repositories
{
    public class LessonRepository : BaseRepository<Lesson>, ILessonRepository
    {
        public LessonRepository(OnlineCoursePlatformDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Lesson>> GetLessonsByCourseIdAsync(Guid courseId)
        {
            var lessons = await _dbContext.Lessons.Where(c => c.CourseId == courseId).OrderBy(l => l.Order).ToListAsync();
            return lessons;
        }
    }
}
