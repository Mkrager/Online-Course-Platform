using Moq;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Features.CoursePublishRequests.Commands.CreateCoursePublishRequest;
using OnlineCoursePlatform.Application.Features.CoursePublishRequests.Commands.UpdateCoursePublishRequestStatus.ApproveCourse;
using OnlineCoursePlatform.Application.Features.CoursePublishRequests.Commands.UpdateCoursePublishRequestStatus.RejectCourse;
using OnlineCoursePlatform.Application.UnitTests.Mocks;
using OnlineCoursePlatform.Domain.Enums;
using Shouldly;

namespace OnlineCoursePlatform.Application.UnitTests.CoursePublishRequests.Commands
{
    public class RejectCoursePublishRequestCommandTest
    {
        private readonly Mock<ICoursePublishRequestRepository> _mockCoursePublishRequestRepository;

        public RejectCoursePublishRequestCommandTest()
        {
            _mockCoursePublishRequestRepository = CoursePublishRequestRepositoryMock.GetCoursePublishRequest();
        }

        [Fact]
        public async Task RejectCourseRequest_ValidCommand_UpdatesStatusPropertySuccessfully()
        {
            var handler = new RejectCoursePublishRequestCommandHandler(_mockCoursePublishRequestRepository.Object);

            var command = new RejectCoursePublishRequestCommand()
            {
                Id = Guid.Parse("6ba684fb-e3bc-418c-8971-2f302b43daf6"),
                RejectReason = "reason"
            };

            var result = await handler.Handle(command, CancellationToken.None);

            var coursePublishRequests = await _mockCoursePublishRequestRepository.Object.ListAllAsync();

            var updCoursePublishRequests = coursePublishRequests.FirstOrDefault(a => a.Id == command.Id);
            updCoursePublishRequests.ShouldNotBeNull();
            updCoursePublishRequests.Id.ShouldBe(command.Id);
            updCoursePublishRequests.Status.ShouldBe(CoursePublishStatus.Rejected);
            updCoursePublishRequests.RejectReason.ShouldBe("reason");
        }

        [Fact]
        public async void Validator_ShouldHaveError_WhenRejectReasonEmpty()
        {
            var validator = new RejectCoursePublishRequestValidator();
            var query = new RejectCoursePublishRequestCommand
            {
                Id = Guid.Parse("6ba684fb-e3bc-418c-8971-2f302b43daf6"),
                RejectReason = ""
            };

            var result = await validator.ValidateAsync(query);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, f => f.PropertyName == "RejectReason");
        }

    }
}