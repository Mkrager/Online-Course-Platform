using Moq;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Features.CoursePublishRequests.Commands.UpdateCoursePublishRequestStatus.CancelCourse;
using OnlineCoursePlatform.Application.UnitTests.Mocks;
using OnlineCoursePlatform.Domain.Entities;
using OnlineCoursePlatform.Domain.Enums;
using Shouldly;

namespace OnlineCoursePlatform.Application.UnitTests.TeacherApplications.Commands
{
    public class CancelTeacherApplicationCommandTest
    {
        private readonly Mock<IRequestRepository<TeacherApplication>> _mockTeacherApplicationRepository;
        public CancelTeacherApplicationCommandTest()
        {
            _mockTeacherApplicationRepository = TeacherApplicationRepositoryMock.GetTeacherApplicationRepository();
        }

        [Fact]
        public async Task CancelTeacherApplication_ValidCommand_UpdatesStatusPropertySuccessfully()
        {
            var handler = new CancelTeacherApplicationCommandHandler(_mockTeacherApplicationRepository.Object);

            var command = new CancelTeacherApplicationCommand()
            {
                Id = Guid.Parse("6ba684fb-e3bc-418c-8971-2f302b43daf6")
            };

            var result = await handler.Handle(command, CancellationToken.None);

            var teacherApplication = await _mockTeacherApplicationRepository.Object.ListAllAsync();

            var updteacherApplication = teacherApplication.FirstOrDefault(a => a.Id == command.Id);
            updteacherApplication.ShouldNotBeNull();
            updteacherApplication.Id.ShouldBe(command.Id);
            updteacherApplication.Status.ShouldBe(RequestStatus.Canceled);
        }
    }
}