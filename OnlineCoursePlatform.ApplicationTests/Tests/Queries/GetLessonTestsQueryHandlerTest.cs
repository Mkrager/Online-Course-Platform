using AutoMapper;
using Moq;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Features.Tests.Queries.GetUserTestsList;
using OnlineCoursePlatform.Application.Profiles;
using OnlineCoursePlatform.Application.UnitTests.Mocks;
using Shouldly;

namespace OnlineCoursePlatform.Application.UnitTests.Tests.Queries
{
    public class GetLessonTestsQueryHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<ITestRepository> _testRepository;

        public GetLessonTestsQueryHandlerTest()
        {
            _testRepository = RepositoryMocks.GetTestRepository();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task GetLessonTests_RetursnListOfTests()
        {
            var handler = new GetLessonTestsQueryHandler(_mapper, _testRepository.Object);

            var result = await handler.Handle(new GetLessonTestsQuery() { LessonId = Guid.Parse("3f29e1a5-67b4-47f2-a726-05e45bdb2b4c") }, CancellationToken.None);

            result.ShouldBeOfType<List<LessonTestListVm>>();

            result.Count.ShouldBe(1);
        }
    }
}
