using Moq;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Features.Courses.Queries.GetCoursesByCategory;
using OnlineCoursePlatform.Application.UnitTests.Base;
using OnlineCoursePlatform.Application.UnitTests.Mocks;

namespace OnlineCoursePlatform.Application.UnitTests.Courses.Queries
{
    public class GetCoursesByCategoryQueryHandlerTest : TestBase
    {
        private readonly Mock<ICourseRepository> _mockCourseRepository;

        public GetCoursesByCategoryQueryHandlerTest()
        {
            _mockCourseRepository = CourseRepositoryMock.GetCourseRepository();
        }

        [Fact]
        public async Task GetCoursesByCategoryId_ReturnsListOfCoursesByCategoryId()
        {
            var handler = new GetCoursesByCategoryQueryHandler(_mapper, _mockCourseRepository.Object);

            var result = await handler.Handle(new GetCoursesByCategoryQuery() { CategoryId = Guid.Parse("2d6e6fbe-3d9f-4a75-a262-2f2b197b4c6a") }, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Single(result);
        }
    }
}