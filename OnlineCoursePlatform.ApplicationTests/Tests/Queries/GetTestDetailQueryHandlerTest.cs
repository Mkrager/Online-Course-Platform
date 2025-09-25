using Moq;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Features.Tests.Queries.GetTestDetail;
using OnlineCoursePlatform.Application.UnitTests.Base;
using OnlineCoursePlatform.Application.UnitTests.Mocks;
using Shouldly;

namespace OnlineCoursePlatform.Application.UnitTests.Tests.Queries
{
    public class GetTestDetailQueryHandlerTest : TestBase
    {
        private readonly Mock<ITestRepository> _mockTestRepository;

        public GetTestDetailQueryHandlerTest()
        {
            _mockTestRepository = TestRepositoryMock.GetTestRepository();
        }

        [Fact]
        public async Task GetTestListWithQuestionAndAnswer_RetursnListOfTests()
        {

            var handler = new GetTestDetailQueryHandler(_mapper, _mockTestRepository.Object);

            var result = await handler.Handle(new GetTestDetailQuery() { Id = Guid.Parse("3f2a3a3e-27c9-4b65-bfb4-2b1e3d4b54ee") }, CancellationToken.None);

            result.ShouldBeOfType<TestDetailVm>();

            result.Title.ShouldBe("Test1");
            result.Id.ShouldBe(Guid.Parse("3f2a3a3e-27c9-4b65-bfb4-2b1e3d4b54ee"));
        }

    }
}
