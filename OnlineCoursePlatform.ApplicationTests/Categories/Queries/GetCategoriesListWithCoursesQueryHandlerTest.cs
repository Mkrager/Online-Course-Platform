using Moq;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Features.Categories.Queries.GetCategoriesListWithCourses;
using OnlineCoursePlatform.Application.UnitTests.Base;
using OnlineCoursePlatform.Application.UnitTests.Mocks;
using Shouldly;

namespace OnlineCoursePlatform.Application.UnitTests.Categories.Queries
{
    public class GetCategoriesListWithCoursesQueryHandlerTest : TestBase
    {
        private readonly Mock<ICategoryRepository> _mockCategoryRepository;

        public GetCategoriesListWithCoursesQueryHandlerTest()
        {
            _mockCategoryRepository = CategoryRepositoryMock.GetCategoryRepository();
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
