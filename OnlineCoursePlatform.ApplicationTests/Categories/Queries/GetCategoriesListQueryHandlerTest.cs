using AutoMapper;
using Moq;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Features.Categories.Queries.GetCategoriesList;
using OnlineCoursePlatform.Application.Features.Courses.Queries.GetCoursesList;
using OnlineCoursePlatform.Application.Profiles;
using OnlineCoursePlatform.ApplicationTests.Mocks;
using OnlineCoursePlatform.Domain.Entities;
using Shouldly;

namespace OnlineCoursePlatform.Application.UnitTests.Categories.Queries
{
    public class GetCategoriesListQueryHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<ICategoryRepository> _mockCategoryRepository;

        public GetCategoriesListQueryHandlerTest()
        {
            _mockCategoryRepository = RepositoryMocks.GetCategoryRepository();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task GetCategoryList_ReturnsListOfCategories()
        {
            var handler = new GetCategoriesListQueryHandler(_mapper, _mockCategoryRepository.Object);

            var result = await handler.Handle(new GetCategoriesListQuery(), CancellationToken.None);

            result.ShouldBeOfType<List<CategoryListVm>>();

            result.Count.ShouldBe(2);
        }

    }
}
