using Moq;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Features.Enrollments.Queries.GetEnrollmentsByStudent;
using OnlineCoursePlatform.Application.UnitTests.Base;
using OnlineCoursePlatform.Application.UnitTests.Mocks;
using Shouldly;

namespace OnlineCoursePlatform.Application.UnitTests.Enrollments.Queries
{
    public class GetEnrollmentsByStudentQueryHandlerTests : TestBase
    {
        private readonly Mock<IEnrollmentRepository> _mockEnrollmentRepository;

        public GetEnrollmentsByStudentQueryHandlerTests()
        {
            _mockEnrollmentRepository = EnrollmentRepositoryMock.GetEnrollmentRepository();
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
