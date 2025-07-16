using AutoMapper;
using Moq;
using OnlineCoursePlatform.Application.Contracts;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Features.Courses.Commands.CreateCourse;
using OnlineCoursePlatform.Application.Features.Enrollments.Commands.CreateEnrollment;
using OnlineCoursePlatform.Application.Profiles;
using Shouldly;

namespace OnlineCoursePlatform.Application.UnitTests.Enrollments.Commands
{
    public class CreateEnrollmentCommandTests
    {
        private readonly Mock<IEnrollmentRepository> _mockEnrollmentRepository;
        private readonly Mock<ICurrentUserService> _currentUserService;
        private readonly IMapper _mapper;

        public CreateEnrollmentCommandTests()
        {
            _mockEnrollmentRepository = Mocks.RepositoryMocks.GetEnrollmentRepository();
            _currentUserService = Mocks.RepositoryMocks.GetCurrentUserService();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task Should_Create_Enrollment_Successfully()
        {
            var handler = new CreateEnrollmentCommandHandler(_mockEnrollmentRepository.Object, _mapper, _currentUserService.Object);

            var command = new CreateEnrollmentCommand
            {
                CourseId = Guid.Parse("b8c3f27a-7b28-4ae6-94c2-91fdc33b77e8")
            };

            await handler.Handle(command, CancellationToken.None);

            var allCourses = await _mockEnrollmentRepository.Object.ListAllAsync();
            allCourses.Count.ShouldBe(1);

            var createdCourse = allCourses.FirstOrDefault(a => a.CourseId == command.CourseId);
            createdCourse.ShouldNotBeNull();
            createdCourse.CourseId.ShouldBe(command.CourseId);
        }
    }
}
