using Microsoft.EntityFrameworkCore;
using Moq;
using OnlineCoursePlatform.Application.Contracts;
using OnlineCoursePlatform.Persistence;
using OnlineCoursePlatform.Domain.Entities;
using OnlineCoursePlatform.Domain.Enums;
using OnlineCoursePlatform.Persistence.Interceptors;
using OnlineCoursePlatform.Persistence.Repositories;

namespace OnlineCoursePlatfrom.Persistence.InregrationTests
{
    public class RequestRepositoryTests
    {
        private readonly OnlineCoursePlatformDbContext _dbContext;
        private readonly RequestRepository<CoursePublishRequest> _repository;
        private readonly Mock<ICurrentUserService> _currentUserServiceMock;
        private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;
        private readonly string _currentUserId;

        public RequestRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<OnlineCoursePlatformDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _currentUserId = "00000000-0000-0000-0000-000000000000";
            _currentUserServiceMock = new Mock<ICurrentUserService>();
            _currentUserServiceMock.Setup(m => m.UserId).Returns(_currentUserId);

            _auditableEntitySaveChangesInterceptor = new AuditableEntitySaveChangesInterceptor(_currentUserServiceMock.Object);
            _dbContext = new OnlineCoursePlatformDbContext(options, _auditableEntitySaveChangesInterceptor);
            _repository = new RequestRepository<CoursePublishRequest>(_dbContext);
        }

        [Fact]
        public async Task UpdateStatusAsync_ShouldUpdateStatusProperty()
        {
            var coursePublishRequest = new CoursePublishRequest
            {
                Id = Guid.Parse("5c711b2d-55b2-4624-8881-7c4b5013f40f"),
                Status = RequestStatus.Pending,
            };

            await _repository.AddAsync(coursePublishRequest);

            await _repository.UpdateStatusAsync(coursePublishRequest, RequestStatus.Approved);

            var updatedCoursePublishRequest = await _dbContext.CoursePublishRequests.FindAsync(coursePublishRequest.Id);
            Assert.NotNull(updatedCoursePublishRequest);
            Assert.Equal(RequestStatus.Approved, updatedCoursePublishRequest.Status);
        }

        [Fact]
        public async Task GetCoursePublishRequestsByUserId_WhenUserHasCoursePublishRequests_ReturnsUserCoursePublishRequests()
        {
            var courseId = Guid.NewGuid();

            var levelId = Guid.NewGuid();

            var level = new Level
            {
                Id = levelId,
                Name = "Test"
            };

            var categoryId = Guid.NewGuid();

            var category = new Category
            {
                Id = categoryId,
                Name = "Test",
            };

            var course = new Course
            {
                Id = courseId,
                LevelId = levelId,
                CategoryId = categoryId,
                Title = "TestCourse",
                Level = level,
                Category = category,
            };

            var coursePublishRequest = new CoursePublishRequest
            {
                CourseId = courseId,
            };

            var coursePublishRequest2 = new CoursePublishRequest
            {
                CourseId = courseId,
            };

            _dbContext.Courses.Add(course);
            _dbContext.CoursePublishRequests.Add(coursePublishRequest);
            _dbContext.CoursePublishRequests.Add(coursePublishRequest2);
            await _dbContext.SaveChangesAsync();

            var result = await _repository.GetRequestByUserIdAsync(_currentUserId);

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }
        [Fact]
        public async Task GetCoursePublishRequestsByUserIdAndStatus_WhenUserHasCoursePublishRequestsWithCorrectStatus_ReturnsUserCoursePublishRequests()
        {
            var courseId = Guid.NewGuid();

            var levelId = Guid.NewGuid();

            var level = new Level
            {
                Id = levelId,
                Name = "Test"
            };

            var categoryId = Guid.NewGuid();

            var category = new Category
            {
                Id = categoryId,
                Name = "Test",
            };

            var course = new Course
            {
                Id = courseId,
                LevelId = levelId,
                CategoryId = categoryId,
                Title = "TestCourse",
                Level = level,
                Category = category,
            };

            var coursePublishRequest = new CoursePublishRequest
            {
                CourseId = courseId,
                Status = RequestStatus.Pending,
            };

            var coursePublishRequest2 = new CoursePublishRequest
            {
                CourseId = courseId,
                Status = RequestStatus.Pending,
            };


            _dbContext.Courses.Add(course);
            _dbContext.CoursePublishRequests.Add(coursePublishRequest);
            _dbContext.CoursePublishRequests.Add(coursePublishRequest2);
            await _dbContext.SaveChangesAsync();

            var result = await _repository.GetRequestsByUserIdAndStatusAsync(_currentUserId, RequestStatus.Pending);

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task GetCoursePublishRequests_oursePublishRequestsHasCorrectStatus_ReturnsFilteredCoursePublishRequests()
        {
            var courseId = Guid.NewGuid();

            var levelId = Guid.NewGuid();

            var level = new Level
            {
                Id = levelId,
                Name = "Test"
            };

            var categoryId = Guid.NewGuid();

            var category = new Category
            {
                Id = categoryId,
                Name = "Test",
            };

            var course = new Course
            {
                Id = courseId,
                LevelId = levelId,
                CategoryId = categoryId,
                Title = "TestCourse",
                Level = level,
                Category = category,
            };

            var coursePublishRequest = new CoursePublishRequest
            {
                CourseId = courseId,
            };

            var coursePublishRequest2 = new CoursePublishRequest
            {
                CourseId = courseId,
            };

            var coursePublishRequest3 = new CoursePublishRequest
            {
                CourseId = courseId,
            };

            _dbContext.Courses.Add(course);
            _dbContext.CoursePublishRequests.AddRange(coursePublishRequest, coursePublishRequest2, coursePublishRequest3);
            await _dbContext.SaveChangesAsync();

            coursePublishRequest.Status = RequestStatus.Approved;

            _dbContext.CoursePublishRequests.Update(coursePublishRequest);
            await _dbContext.SaveChangesAsync();

            var result = await _repository.GetRequestsByStatusAsync(RequestStatus.Pending);

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }
    }
}
