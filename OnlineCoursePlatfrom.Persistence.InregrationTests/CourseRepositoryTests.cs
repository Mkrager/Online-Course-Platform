using Microsoft.EntityFrameworkCore;
using Moq;
using OnlineCoursePlatform.Application.Contracts;
using OnlineCoursePlatform.Domain.Entities;
using OnlineCoursePlatform.Persistence;
using OnlineCoursePlatform.Persistence.Repositories;

namespace OnlineCoursePlatfrom.Persistence.InregrationTests
{
    public class CourseRepositoryTests
    {
        private readonly OnlineCoursePlatformDbContext _dbContext;
        private readonly CourseRepository _repository;
        private readonly Mock<ICurrentUserService> _currentUserServiceMock;
        private readonly string _currentUserId;

        public CourseRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<OnlineCoursePlatformDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _currentUserId = "00000000-0000-0000-0000-000000000000";
            _currentUserServiceMock = new Mock<ICurrentUserService>();
            _currentUserServiceMock.Setup(m => m.UserId).Returns(_currentUserId);

            _dbContext = new OnlineCoursePlatformDbContext(options, _currentUserServiceMock.Object);

            _repository = new CourseRepository(_dbContext);
        }

        [Fact]
        public async Task GetCoursesByUserId_WhenUserHasCourses_ReturnsUserCourses()
        {
            var courseId = Guid.NewGuid();
            var course = new Course
            {
                Id = courseId,
                Title = "TestCourse"
            };

            _dbContext.Courses.Add(course);
            await _dbContext.SaveChangesAsync();

            var result = await _repository.GetCoursesByUserId("00000000-0000-0000-0000-000000000000");

            Assert.NotNull(result);
            Assert.Equal(1, result.Count);

        }
    }
}
