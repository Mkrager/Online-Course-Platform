using AutoMapper;
using Moq;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Features.Courses.Queries.GetCoursesList;
using OnlineCoursePlatform.Application.Features.Enrollments.Queries.GetEnrollmentsByStudent;
using OnlineCoursePlatform.Application.Profiles;
using OnlineCoursePlatform.Application.UnitTests.Mocks;
using Shouldly;

namespace OnlineCoursePlatform.Application.UnitTests.Enrollments.Queries
{
    public class GetEnrollmentsByStudentQueryHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IEnrollmentRepository> _mockEnrollmentRepository;

        public GetEnrollmentsByStudentQueryHandlerTests()
        {
            _mockEnrollmentRepository = EnrollmentRepositoryMock.GetEnrollmentRepository();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task GetEnrollmentsListByUser_ReturnsListOfEnrollments()
        {
            var handler = new GetEnrollmentsByStudentQueryHandler(_mapper, _mockEnrollmentRepository.Object);

            var result = await handler.Handle(new GetEnrollmentsByStudentQuery() { UserId = "123"}, CancellationToken.None);

            result.ShouldBeOfType<List<StudentEnrollmentsListVm>>();

            result.Count.ShouldBe(2);
            result[0].Course.ShouldNotBeNull();
        }

    }
}
