using Moq;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Features.Courses.Queries.GetCourseDetail;
using OnlineCoursePlatform.Application.UnitTests.Base;
using OnlineCoursePlatform.Application.UnitTests.Mocks;
using Shouldly;

namespace OnlineCoursePlatform.ApplicationTests.Courses.Queries
{
    public class GetCourseDetailsQueryHandlerTest : TestBase
    {
        private readonly Mock<ICourseRepository> _mockCourseRepository;

        public GetCourseDetailsQueryHandlerTest()
        {
            _mockCourseRepository = CourseRepositoryMock.GetCourseRepository();
        }

        [Fact]
        public async Task GetCourseDetails_ReturnsCorrectCourseDetails()
        {
            var handler = new GetCourseDetailQueryHandler(_mapper, _mockCourseRepository.Object);

            var result = await handler.Handle(new GetCourseDetailQuery() { Id = Guid.Parse("b8c3f27a-7b28-4ae6-94c2-91fdc33b77e8") }, CancellationToken.None);

            result.ShouldBeOfType<CourseDetailVm>();

            result.Id.ShouldBe(Guid.Parse("b8c3f27a-7b28-4ae6-94c2-91fdc33b77e8"));
            result.Title.ShouldBe("test");
            result.Description.ShouldBe("test");
            result.Price.ShouldBe(100);
            result.ThumbnailUrl.ShouldBe("test");
        }
    }
}
