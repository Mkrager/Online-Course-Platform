using Microsoft.EntityFrameworkCore;
using OnlineCoursePlatform.Domain.Entities;
using OnlineCoursePlatform.Persistence.Repositories;
using OnlineCoursePlatform.Persistence;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;

namespace OnlineCoursePlatfrom.Persistence.InregrationTests
{
    public class CategoryRepositoryTests
    {
        private readonly OnlineCoursePlatformDbContext _dbContext;
        private readonly CategoryRepository _repository;

        public CategoryRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<OnlineCoursePlatformDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            _dbContext = new OnlineCoursePlatformDbContext(options);
            _repository = new CategoryRepository(_dbContext);
        }

        [Fact]
        public async Task GetCategoryWithCourses_ReturnsCategoriesWithCourses()
        {
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

            var result = await _repository.GetCategoriesWithCourses();

            Assert.NotNull(result);
            Assert.Single(result);
            var returnedCategory = result.First();
            Assert.Equal(category.Name, returnedCategory.Name);
            Assert.Equal(2, returnedCategory.Courses.Count);
        }

        [Fact]
        public async Task CheckIsCategoryNameUnique_ReturnsFalseWhenNotUnique()
        {
            var category1Id = Guid.NewGuid();
            var category2Id = Guid.NewGuid();

            var category1 = new Category { Id = category1Id, Name = "TestName" };

            _dbContext.Categories.Add(category1);
            await _dbContext.SaveChangesAsync();

            var result = await _repository.IsCategoryNameUnique("TestName");
            Assert.False(result);
        }

        [Fact]
        public async Task CheckIsCategoryNameUnique_ReturnsTrueWhenUnique()
        {
            var category1Id = Guid.NewGuid();
            var category2Id = Guid.NewGuid();

            var category1 = new Category { Id = category1Id, Name = "TestName1" };

            _dbContext.Categories.Add(category1);
            await _dbContext.SaveChangesAsync();

            var result = await _repository.IsCategoryNameUnique("TestName");
            Assert.True(result);
        }
    }
}
