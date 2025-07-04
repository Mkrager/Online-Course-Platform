using AutoMapper;
using Moq;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Features.Categories.Queries.GetCategoriesListWithCourses;
using OnlineCoursePlatform.Application.Profiles;
using OnlineCoursePlatform.Application.UnitTests.Mocks;
using Shouldly;

namespace OnlineCoursePlatform.Application.UnitTests.Categories.Queries
{
    public class GetCategoriesListWithCoursesQueryHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<ICategoryRepository> _mockCategoryRepository;

        public GetCategoriesListWithCoursesQueryHandlerTest()
        {
            _mockCategoryRepository = RepositoryMocks.GetCategoryRepository();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task GetCategoriesListWithCourses()
        {

            var handler = new GetCategoriesListWithCoursesQueryHandler(_mapper, _mockCategoryRepository.Object);

            var result = await handler.Handle(new GetCategoriesListWithCoursesQuery(), CancellationToken.None);

            result.ShouldBeOfType<List<CategoryCourseListVm>>();

            result.Count.ShouldBe(2);
        }

    }
}
