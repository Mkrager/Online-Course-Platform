using MediatR;
using Moq;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Features.CoursePublishRequests.Commands.UpdateCoursePublishRequestStatus.ApproveCourse;
using OnlineCoursePlatform.Application.Features.TeacherApplications.Commands.UpdateCoursePublishRequestStatus.ApproveTeacher;
using OnlineCoursePlatform.Application.UnitTests.Mocks;
using OnlineCoursePlatform.Domain.Entities;
using OnlineCoursePlatform.Domain.Enums;
using Shouldly;

namespace OnlineCoursePlatform.Application.UnitTests.TeacherApplications.Commands
{
    public class ApproveTeacherApplicationCommandTest
    {
        private readonly Mock<IRequestRepository<TeacherApplication>> _mockTeacherApplicationRepository;
        private readonly Mock<IMediator> _mockMediator;
        public ApproveTeacherApplicationCommandTest()
        {
            _mockTeacherApplicationRepository = TeacherApplicationRepositoryMock.GetTeacherApplicationRepository();
            _mockMediator = MediatorMock.GetMediator();
        }

        [Fact]
        public async Task AproveTeacherApplication_ValidCommand_UpdatesStatusPropertySuccessfully()
        {
            var handler = new ApproveTeacherApplicationCommandHandler(_mockTeacherApplicationRepository.Object, _mockMediator.Object);

            var command = new ApproveTeacherApplicationCommand()
            {
                Id = Guid.Parse("6ba684fb-e3bc-418c-8971-2f302b43daf6")
            };

            var result = await handler.Handle(command, CancellationToken.None);

            var teacherApplication = await _mockTeacherApplicationRepository.Object.ListAllAsync();

            var updteacherApplication = teacherApplication.FirstOrDefault(a => a.Id == command.Id);
            updteacherApplication.ShouldNotBeNull();
            updteacherApplication.Id.ShouldBe(command.Id);
            updteacherApplication.Status.ShouldBe(RequestStatus.Approved);
        }
    }
}
