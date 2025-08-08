using Microsoft.EntityFrameworkCore;
using Moq;
using OnlineCoursePlatform.Application.Contracts;
using OnlineCoursePlatform.Persistence.Repositories;
using OnlineCoursePlatform.Persistence;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatfrom.Persistence.InregrationTests
{
    public class EnrollmentRepositoryTests
    {
        private readonly OnlineCoursePlatformDbContext _dbContext;
        private readonly EnrollmentRepository _repository;
        private readonly Mock<ICurrentUserService> _currentUserServiceMock;
        private readonly string _currentUserId;

        public EnrollmentRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<OnlineCoursePlatformDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _currentUserId = "12300000-0000-0000-0000-000000000000";
            _currentUserServiceMock = new Mock<ICurrentUserService>();
            _currentUserServiceMock.Setup(m => m.UserId).Returns(_currentUserId);

            _dbContext = new OnlineCoursePlatformDbContext(options, _currentUserServiceMock.Object);

            _repository = new EnrollmentRepository(_dbContext);
        }

        [Fact]
        public async Task CheckIUserIdAndCategoryIdUnique_ReturnsTrueWhenUserHaveThisCourse()
        {
            var enrollmentId = Guid.NewGuid();
            var courseId = Guid.Parse("d9659c7e-c8ea-418f-a8c6-b51c1ad4ca80");

            var enrollment1 = new Enrollment
            {
                Id = enrollmentId,
                StudentId = "testUserID",
                CourseId = courseId,
            };

            _dbContext.Enrollments.Add(enrollment1);
            await _dbContext.SaveChangesAsync();

            var result = await _repository.IsUserEnrolledInCourseAsync("testUserID", courseId);
            Assert.True(result);
        }

        [Fact]
        public async Task GetEnrollmentWithCoursesByStudentId_ShouldReturnListOfEnrollment()
        {
            var courseId = Guid.NewGuid();

            var course = new Course 
            { 
                Id = courseId 
            };

            _dbContext.Courses.Add(course);
            await _dbContext.SaveChangesAsync();

            var enrollment = new Enrollment
            {
                Id = Guid.NewGuid(),
                StudentId = "123",
                CourseId = courseId
            };

            _dbContext.Enrollments.Add(enrollment);
            await _dbContext.SaveChangesAsync();

            var result = await _repository.GetEnrollmentsByStudentIdWithCoursesAsync("123");

            Assert.NotNull(result);
            Assert.NotNull(result[0].Course);
            Assert.Equal(1, result.Count);
        }

    }
}
