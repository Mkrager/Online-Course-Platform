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

        public async Task<List<Course>> GetCoursesWithCategoryAndLevelAsync()
        {
            return await _dbContext.Courses
                .Include(c => c.Category)
                .Include(c => c.Level)
                .ToListAsync();
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
    }
}
