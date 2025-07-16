using Microsoft.EntityFrameworkCore;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Persistence.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(OnlineCoursePlatformDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Category>> GetCategoriesWithCourses()
        {
            var allCategories = await _dbContext.Categories.Include(x => x.Courses).ToListAsync();
            return allCategories;
        }

        public async Task<bool> IsCategoryNameUnique(string name)
        {
            var matches = await _dbContext.Categories.AnyAsync(x => x.Name.Equals(name));
            return !matches;
        }
    }
}
