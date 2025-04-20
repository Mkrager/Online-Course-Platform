using Microsoft.EntityFrameworkCore;
using OnlineCoursePlatform.Domain.Entities;
using OnlineCoursePlatform.Persistence.Repositories;
using OnlineCoursePlatform.Persistence;

namespace OnlineCoursePlatfrom.Persistence.InregrationTests
{
    public class CategoryRepositoryTests
    {
        private readonly OnlineCoursePlatformDbContext _dbContext;
        private readonly CategoryRepository _repository;

        public CategoryRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<OnlineCoursePlatformDbContext>()
                .UseInMemoryDatabase(databaseName: "RestinoDb")
                .Options;
            _dbContext = new OnlineCoursePlatformDbContext(options);
            _repository = new CategoryRepository(_dbContext);
        }

        [Fact]
        public async Task GetCategoryWithCourses_ReturnsCategoriesWithCourses__WhenOnlyOneCategoryResultIsFalse()
        {
            // Arrange
            var categoryId = Guid.NewGuid();
            var category = new Category
            {
                Id = categoryId,
                Name = "Test Category"
            };

            var courseTest1 = new Course { Title = "CourseTest 1", CategoryId = categoryId };
            var courseTest2 = new Course { Title = "CourseTest 2", CategoryId = categoryId };

            category.Courses = new List<Course> { courseTest1, courseTest2 };

            _dbContext.Categories.Add(category);
            _dbContext.Courses.AddRange(courseTest1, courseTest2);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _repository.GetCategoriesWithCourses();

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            var returnedCategory = result.First();
            Assert.Equal(category.Name, returnedCategory.Name);
            Assert.Equal(2, returnedCategory.Courses.Count);
        }

    }
}
