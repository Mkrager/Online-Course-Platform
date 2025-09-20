using Microsoft.EntityFrameworkCore;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Persistence.Repositories
{
    public class CourseRepository : BaseRepository<Course>, ICourseRepository
    {
        public CourseRepository(OnlineCoursePlatformDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Course>> GetCoursesWithCategoryAndLevelAsync(bool onlyPublished = false)
        {
            var query = _dbContext.Courses
                    .Include(c => c.Category)
                    .Include(c => c.Level)
                    .AsQueryable();

            if (onlyPublished)
                query = query.Where(r => r.IsPublished);


            return await query.ToListAsync();
        }

        public async Task<List<Course>> GetCoursesByCategoryIdAsync(Guid categoryId)
        {
            return await _dbContext.Courses
                .Include(c => c.Category)
                .Include(c => c.Level)
                .Where(c => c.CategoryId == categoryId)
                .OrderBy(c => c.CreatedDate)
                .ToListAsync();
        }

        public async Task<List<Course>> GetCoursesByUserIdAsync(string userId)
        {
            return await _dbContext.Courses
                .Include(c => c.Category)
                .Include(c => c.Level)
                .Where(c => c.CreatedBy == userId)
                .ToListAsync();
        }

        public async Task UpdateIsPublishedAsync(Course course, bool isPublished)
        {
            course.IsPublished = isPublished;
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Course?> GetCourseByIdWithCategoryAndLevelAsync(Guid id)
        {
            return await _dbContext.Courses
                .Include(r => r.Category)
                .Include(c => c.Level)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<bool> IsUserCourseTeacherAsync(string userId, Guid courseId)
        {
            var matches = await _dbContext.Courses.AnyAsync(x => x.CreatedBy == userId && x.Id == courseId);
            return matches;
        }
    }
}