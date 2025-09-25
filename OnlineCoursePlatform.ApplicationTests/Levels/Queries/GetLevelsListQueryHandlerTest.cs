using Moq;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Features.Levels.Queries.GetLevelsList;
using OnlineCoursePlatform.Application.UnitTests.Base;
using OnlineCoursePlatform.Application.UnitTests.Mocks;
using OnlineCoursePlatform.Domain.Entities;
using Shouldly;

namespace OnlineCoursePlatform.Application.UnitTests.Levels.Queries
{
    public class GetLevelsListQueryHandlerTest : TestBase
    {
        private readonly Mock<IAsyncRepository<Level>> _mockLevelRepository;

        public GetLevelsListQueryHandlerTest()
        {
            _mockLevelRepository = LevelRepositoryMock.GetLevelRepository();
        }

        [Fact]
        public async Task GetLevelList_ReturnsListOfLevels()
        {
            var handler = new GetLevelsListQueryHandler(_mapper, _mockLevelRepository.Object);

            var result = await handler.Handle(new GetLevelsListQuery(), CancellationToken.None);

            result.ShouldBeOfType<List<LevelListVm>>();

            result.Count.ShouldBe(2);
        }
    }
}
