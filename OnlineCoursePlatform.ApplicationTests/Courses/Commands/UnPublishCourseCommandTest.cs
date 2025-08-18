using Moq;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Features.Courses.Commands.UnPublishCourse;
using OnlineCoursePlatform.Application.UnitTests.Mocks;
using Shouldly;

namespace OnlineCoursePlatform.Application.UnitTests.Courses.Commands
{
    public class UnPublishCourseCommandTest
    {
        private readonly Mock<ICourseRepository> _mockCourseRepository;

        public UnPublishCourseCommandTest()
        {
            _mockCourseRepository = CourseRepositoryMock.GetCourseRepository();
        }

        [Fact]
        public async Task UnPublishCourse_ValidCommand_UpdatesIsPublishPropertySuccessfully()
        {
            var handler = new UnPublishCourseCommandHandler(_mockCourseRepository.Object);
            var updateCommand = new UnPublishCourseCommand
            {
                Id = Guid.Parse("b8c3f27a-7b28-4ae6-94c2-91fdc33b77e8"),
            };

            await handler.Handle(updateCommand, CancellationToken.None);

            var updatedCourse = await _mockCourseRepository.Object.GetByIdAsync(updateCommand.Id);

            updatedCourse.ShouldNotBeNull();
            updatedCourse.IsPublished.ShouldBeFalse();
        }
    }
}
