using Moq;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Features.CoursePublishRequests.Commands.CreateCoursePublishRequest;
using OnlineCoursePlatform.Application.Features.TeacherApplications.Commands.CreateTeacherApplication;
using OnlineCoursePlatform.Application.UnitTests.Base;
using OnlineCoursePlatform.Application.UnitTests.Mocks;
using OnlineCoursePlatform.Domain.Entities;
using Shouldly;

namespace OnlineCoursePlatform.Application.UnitTests.TeacherApplications.Commands
{
    public class CreateTeacherApplicationCommandTest : TestBase
    {
        private readonly Mock<IRequestRepository<TeacherApplication>> _mockTeacherRepository;

        public CreateTeacherApplicationCommandTest()
        {
            _mockTeacherRepository = TeacherApplicationRepositoryMock.GetTeacherApplicationRepository();
        }

        [Fact]
        public async Task Should_Create_TeacherApplication_Successfully()
        {
            var handler = new CreateTeacherApplicationCommandHandler(_mockTeacherRepository.Object, _mapper);

            var command = new CreateTeacherApplicationCommand()
            {
                Bio = "gejrgehjskgeshrgjkeshrgjkrehjksgjser",
                Experience = "disjflahjjksfheajkfhsejkfhesjkfaejklf"
            };

            var result = await handler.Handle(command, CancellationToken.None);

            var allTeacherApplications = await _mockTeacherRepository.Object.ListAllAsync();
            allTeacherApplications.Count.ShouldBe(3);

            var createdCourse = allTeacherApplications.FirstOrDefault(a => a.Experience == command.Experience);
            createdCourse.ShouldNotBeNull();
            createdCourse.Experience.ShouldBe(command.Experience);
        }

    }
}
