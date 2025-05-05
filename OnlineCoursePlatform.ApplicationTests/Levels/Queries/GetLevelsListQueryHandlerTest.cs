using AutoMapper;
using Moq;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Features.Levels.Queries.GetLevelsList;
using OnlineCoursePlatform.Application.Profiles;
using OnlineCoursePlatform.ApplicationTests.Mocks;
using OnlineCoursePlatform.Domain.Entities;
using Shouldly;

namespace OnlineCoursePlatform.Application.UnitTests.Levels.Queries
{
    public class GetLevelsListQueryHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IAsyncRepository<Level>> _mockLLevelRepository;

        public GetLevelsListQueryHandlerTest()
        {
            _mockLLevelRepository = RepositoryMocks.GetLevelRepository();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task GetLevelList_ReturnsListOfLevels()
        {
            var handler = new GetLevelsListQueryHandler(_mapper, _mockLLevelRepository.Object);

            var result = await handler.Handle(new GetLevelsListQuery(), CancellationToken.None);

            result.ShouldBeOfType<List<LevelListVm>>();

            result.Count.ShouldBe(2);
        }
    }
}
