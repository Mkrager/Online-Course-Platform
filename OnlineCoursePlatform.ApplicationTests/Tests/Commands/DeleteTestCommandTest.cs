using Moq;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Features.Tests.Commands.DeleteTest;
using OnlineCoursePlatform.Application.UnitTests.Base;
using OnlineCoursePlatform.Application.UnitTests.Mocks;
using Shouldly;

namespace OnlineCoursePlatform.Application.UnitTests.Tests.Commands
{
    public class DeleteTestCommandTest : TestBase
    {
        private readonly Mock<ITestRepository> _mockTestRepository;

        public DeleteTestCommandTest()
        {
            _mockTestRepository = TestRepositoryMock.GetTestRepository();
        }

        [Fact]
        public async Task Should_Delete_Test_Successfully()
        {
            var handler = new DeleteTestCommandHandler(_mapper, _mockTestRepository.Object);

            var result = handler.Handle(new DeleteTestCommand() { Id = Guid.Parse("3f2a3a3e-27c9-4b65-bfb4-2b1e3d4b54ee") }, CancellationToken.None);

            var allTests = await _mockTestRepository.Object.ListAllAsync();

            allTests.Count.ShouldBe(1);
        }
    }
}