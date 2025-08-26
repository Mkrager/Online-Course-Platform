using Microsoft.EntityFrameworkCore;
using Moq;
using OnlineCoursePlatform.Application.Contracts;
using OnlineCoursePlatform.Persistence.Repositories;
using OnlineCoursePlatform.Persistence;
using OnlineCoursePlatform.Domain.Entities;
using OnlineCoursePlatform.Domain.Enums;

namespace OnlineCoursePlatfrom.Persistence.InregrationTests
{
    public class CoursePublishRequestRepositoryTests
    {
        private readonly OnlineCoursePlatformDbContext _dbContext;
        private readonly CoursePublishRequestRepository _repository;
        private readonly Mock<ICurrentUserService> _currentUserServiceMock;
        private readonly string _currentUserId;

        public CoursePublishRequestRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<OnlineCoursePlatformDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _currentUserId = "00000000-0000-0000-0000-000000000000";
            _currentUserServiceMock = new Mock<ICurrentUserService>();
            _currentUserServiceMock.Setup(m => m.UserId).Returns(_currentUserId);

            _dbContext = new OnlineCoursePlatformDbContext(options, _currentUserServiceMock.Object);
            _repository = new CoursePublishRequestRepository(_dbContext);
        }

        [Fact]
        public async Task UpdateStatusAsync_ShouldUpdateStatusProperty()
        {
            var coursePublishRequest = new CoursePublishRequest
            {
                Id = Guid.Parse("5c711b2d-55b2-4624-8881-7c4b5013f40f"),
                Status = CoursePublishStatus.Pending,
            };

            await _repository.AddAsync(coursePublishRequest);

            await _repository.UpdateStatusAsync(coursePublishRequest, CoursePublishStatus.Approved);

            var updatedCoursePublishRequest = await _dbContext.CoursePublishRequests.FindAsync(coursePublishRequest.Id);
            Assert.NotNull(updatedCoursePublishRequest);
            Assert.Equal(CoursePublishStatus.Approved, updatedCoursePublishRequest.Status);
        }
    }
}
