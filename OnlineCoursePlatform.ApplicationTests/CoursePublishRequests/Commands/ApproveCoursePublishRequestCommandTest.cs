using MediatR;
using Moq;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Features.CoursePublishRequests.Commands.UpdateCoursePublishRequestStatus.ApproveCourse;
using OnlineCoursePlatform.Application.UnitTests.Mocks;
using OnlineCoursePlatform.Domain.Enums;
using Shouldly;

namespace OnlineCoursePlatform.Application.UnitTests.CoursePublishRequests.Commands
{
    public class ApproveCoursePublishRequestCommandTest
    {
        private readonly Mock<ICoursePublishRequestRepository> _mockCoursePublishRequestRepository;
        private readonly Mock<ICourseRepository> _mockCourseRepository;
        private readonly Mock<IMediator> _mockMediator;
        public ApproveCoursePublishRequestCommandTest()
        {
            _mockCourseRepository = CourseRepositoryMock.GetCourseRepository();
            _mockCoursePublishRequestRepository = CoursePublishRequestRepositoryMock.GetCoursePublishRequest();
            _mockMediator = MediatorMock.GetMediator();
        }

        [Fact]
        public async Task AproveCourseRequest_ValidCommand_UpdatesStatusPropertySuccessfully()
        {
            var handler = new ApproveCoursePublishRequestCommandHandler(_mockCoursePublishRequestRepository.Object, _mockMediator.Object);

            var command = new ApproveCoursePublishRequestCommand()
            {
                Id = Guid.Parse("6ba684fb-e3bc-418c-8971-2f302b43daf6")
            };

            var result = await handler.Handle(command, CancellationToken.None);

            var coursePublishRequests = await _mockCoursePublishRequestRepository.Object.ListAllAsync();

            var updCoursePublishRequests = coursePublishRequests.FirstOrDefault(a => a.Id == command.Id);
            updCoursePublishRequests.ShouldNotBeNull();
            updCoursePublishRequests.Id.ShouldBe(command.Id);
            updCoursePublishRequests.Status.ShouldBe(CoursePublishStatus.Approved);

            var course = await _mockCourseRepository.Object.GetByIdAsync(Guid.Parse("b8c3f27a-7b28-4ae6-94c2-91fdc33b21e8"));
            course.IsPublished.ShouldBeTrue();
        }
    }
}