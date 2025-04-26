using Microsoft.EntityFrameworkCore;
using Moq;
using OnlineCoursePlatform.Application.Contracts;
using OnlineCoursePlatform.Domain.Entities;
using OnlineCoursePlatform.Persistence;
using OnlineCoursePlatform.Persistence.Repositories;

namespace OnlineCoursePlatfrom.Persistence.InregrationTests
{
    public class BaseRepositoryTests
    {
        private readonly OnlineCoursePlatformDbContext _dbContext;
        private readonly BaseRepositrory<Course> _repository;
        private readonly Mock<ICurrentUserService> _currentUserServiceMock;
        private readonly string _currentUserId;

        public BaseRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<OnlineCoursePlatformDbContext>()
                .UseInMemoryDatabase(databaseName: "OnlineCoursePlatformDb")
                .Options;

            _currentUserId = "00000000-0000-0000-0000-000000000000";
            _currentUserServiceMock = new Mock<ICurrentUserService>();
            _currentUserServiceMock.Setup(m => m.UserId).Returns(_currentUserId);

            _dbContext = new OnlineCoursePlatformDbContext(options, _currentUserServiceMock.Object);
            _repository = new BaseRepositrory<Course>(_dbContext);
        }

        [Fact]
        public async Task AddAsync_ShouldAddEntityToDatabase()
        {
            var course = new Course { Title = "New Course", CreatedDate = DateTime.UtcNow };

            var result = await _repository.AddAsync(course);

            var addedCourse = await _dbContext.Courses.FindAsync(result.Id);
            Assert.NotNull(addedCourse);
            Assert.Equal("New Course", addedCourse.Title);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateEntity()
        {
            var course = new Course { Title = "Old Name", CreatedDate = DateTime.UtcNow };
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
            var course = new Course { Title = "Course to Delete", CreatedDate = DateTime.UtcNow };
            await _repository.AddAsync(course);

            await _repository.DeleteAsync(course);

            var deletedCourse = await _dbContext.Courses.FindAsync(course.Id);
            Assert.Null(deletedCourse);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnEntity_WhenEntityExists()
        {
            var course = new Course { Title = "Course", CreatedDate = DateTime.UtcNow };
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
