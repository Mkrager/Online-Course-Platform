using Microsoft.EntityFrameworkCore;
using Moq;
using OnlineCoursePlatform.Application.Contracts;
using OnlineCoursePlatform.Persistence.Repositories;
using OnlineCoursePlatform.Persistence;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatfrom.Persistence.InregrationTests
{
    public class LessonRepositoryTests
    {
        private readonly OnlineCoursePlatformDbContext _dbContext;
        private readonly LessonRepository _repository;
        private readonly Mock<ICurrentUserService> _currentUserServiceMock;
        private readonly string _currentUserId;

        public LessonRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<OnlineCoursePlatformDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _currentUserId = "00000000-0000-0000-0000-000000000000";
            _currentUserServiceMock = new Mock<ICurrentUserService>();
            _currentUserServiceMock.Setup(m => m.UserId).Returns(_currentUserId);

            _dbContext = new OnlineCoursePlatformDbContext(options, _currentUserServiceMock.Object);

            _repository = new LessonRepository(_dbContext);
        }

        [Fact]
        public async Task GetCourseLessons_ReturnsCourseLessons()
        {
            var courseId = Guid.NewGuid();

            var course = new Course()
            {
                Id = courseId
            };

            _dbContext.Courses.Add(course);

            var lessonId = Guid.NewGuid();

            var lesson = new Lesson()
            {
                Id = lessonId,
                CourseId = courseId,
            };

            _dbContext.Lessons.Add(lesson);

            await _dbContext.SaveChangesAsync();

            var result = await _repository.GetLessonsByCourseIdAsync(courseId);

            Assert.NotNull(result);

            Assert.Equal(1, result.Count);
        }
    }
}
