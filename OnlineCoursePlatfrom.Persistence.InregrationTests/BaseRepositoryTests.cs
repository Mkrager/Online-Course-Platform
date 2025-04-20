using Microsoft.EntityFrameworkCore;
using OnlineCoursePlatform.Domain.Entities;
using OnlineCoursePlatform.Persistence;
using OnlineCoursePlatform.Persistence.Repositories;

namespace OnlineCoursePlatfrom.Persistence.InregrationTests
{
    public class BaseRepositoryTests
    {
        private readonly OnlineCoursePlatformDbContext _dbContext;
        private readonly BaseRepositrory<Course> _repository;

        public BaseRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<OnlineCoursePlatformDbContext>()
                .UseInMemoryDatabase(databaseName: "OnlineCoursePlatformDb")
                .Options;
            _dbContext = new OnlineCoursePlatformDbContext(options);
            _repository = new BaseRepositrory<Course>(_dbContext);
        }

        [Fact]
        public async Task AddAsync_ShouldAddEntityToDatabase()
        {
            var course = new Course { Title = "New Course", CreatedAt = DateTime.UtcNow };

            var result = await _repository.AddAsync(course);

            var addedCourse = await _dbContext.Courses.FindAsync(result.Id);
            Assert.NotNull(addedCourse);
            Assert.Equal("New Course", addedCourse.Title);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateEntity()
        {
            var course = new Course { Title = "Old Name", CreatedAt = DateTime.UtcNow };
            await _repository.AddAsync(course);

            course.Title = "Updated Name";
            await _repository.UpdateAsync(course);

            var updatedCourse = await _dbContext.Courses.FindAsync(course.Id);
            Assert.NotNull(updatedCourse);
            Assert.Equal("Updated Name", updatedCourse.Title);
        }

        [Fact]
        public async Task DeleteAsync_ShouldDeleteEntity()
        {
            var course = new Course { Title = "Course to Delete", CreatedAt = DateTime.UtcNow };
            await _repository.AddAsync(course);

            await _repository.DeleteAsync(course);

            var deletedCourse = await _dbContext.Courses.FindAsync(course.Id);
            Assert.Null(deletedCourse);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnEntity_WhenEntityExists()
        {
            var course = new Course { Title = "Course", CreatedAt = DateTime.UtcNow };
            await _repository.AddAsync(course);

            var result = await _repository.GetByIdAsync(course.Id);

            Assert.NotNull(result);
            Assert.Equal(course.Title, result.Title);
        }

        [Fact]
        public async Task ListAllAsync_ShouldReturnAllEntities()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Database.EnsureCreated();

            var result = await _repository.ListAllAsync();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }
    }
}
