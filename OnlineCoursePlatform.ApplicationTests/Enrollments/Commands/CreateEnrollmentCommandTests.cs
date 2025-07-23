using AutoMapper;
using Moq;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Features.Enrollments.Commands.CreateEnrollment;
using OnlineCoursePlatform.Application.Profiles;
using Shouldly;

namespace OnlineCoursePlatform.Application.UnitTests.Enrollments.Commands
{
    public class CreateEnrollmentCommandTests
    {
        private readonly Mock<IEnrollmentRepository> _mockEnrollmentRepository;
        private readonly IMapper _mapper;

        public CreateEnrollmentCommandTests()
        {
            _mockEnrollmentRepository = Mocks.RepositoryMocks.GetEnrollmentRepository();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task Should_Create_Enrollment_Successfully()
        {
            var handler = new CreateEnrollmentCommandHandler(_mockEnrollmentRepository.Object, _mapper);

            var command = new CreateEnrollmentCommand
            {
                CourseId = Guid.Parse("b8c3f27a-7b28-4ae6-94c2-91fdc33b77e8")
            };

            await handler.Handle(command, CancellationToken.None);

            var allEnrollments = await _mockEnrollmentRepository.Object.ListAllAsync();
            allEnrollments.Count.ShouldBe(2);

            var createdEnrollment = allEnrollments.FirstOrDefault(a => a.CourseId == command.CourseId);
            createdEnrollment.ShouldNotBeNull();
            createdEnrollment.CourseId.ShouldBe(command.CourseId);
        }
    }
}
