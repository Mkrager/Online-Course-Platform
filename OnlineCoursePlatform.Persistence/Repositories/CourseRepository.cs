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

        public async Task<List<Course>> GetAllWithCategoryAndLevel()
        {
            return await _dbContext.Courses
                .Include(c => c.Category)
                .Include(c => c.Level)
                .ToListAsync();
        }

        public async Task<List<Course>> GetCoursesByCategoryId(Guid categoryId)
        {
            return await _dbContext.Courses
                .Include(c => c.Category)
                .Include(c => c.Level)
                .Where(c => c.CategoryId == categoryId)
                .OrderBy(c => c.CreatedDate)
                .ToListAsync();
        }

        public async Task<List<Course>> GetCoursesByUserId(string userId)
        {
            var courses = await _dbContext.Courses.Where(x => x.CreatedBy == userId).ToListAsync();

            return courses;
        }
    }
}
