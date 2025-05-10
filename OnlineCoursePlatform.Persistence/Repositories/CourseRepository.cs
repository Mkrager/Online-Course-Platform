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
                .Where(x => x.CategoryId == categoryId)
                .Select(c => new Course
                {
                    Id = c.Id,
                    Title = c.Title,
                    CreatedDate = c.CreatedDate,
                    Description = c.Description,
                    Price = c.Price,
                    Duration = c.Duration,
                    ThumbnailUrl = c.ThumbnailUrl,
                    CategoryId = c.CategoryId,
                    LevelId = c.LevelId,
                    Category = new Category
                    {
                        Id = c.Category.Id,
                        Name = c.Category.Name
                    },
                    Level = new Level
                    {
                        Id = c.Level.Id,
                        Name = c.Level.Name
                    }
                })
                .ToListAsync();
        }

        public async Task<List<Course>> GetCoursesByUserId(string userId)
        {
            var courses = await _dbContext.Courses.Where(x => x.CreatedBy == userId).ToListAsync();

            return courses;
        }
    }
}
