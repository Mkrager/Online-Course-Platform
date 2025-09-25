using Moq;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Features.Courses.Queries.GetCoursesList;
using OnlineCoursePlatform.Application.UnitTests.Base;
using OnlineCoursePlatform.Application.UnitTests.Mocks;
using Shouldly;

namespace OnlineCoursePlatform.ApplicationTests.Courses.Queries
{
    public class GetCoursesListQueryHandlerTest : TestBase
    {
        private readonly Mock<ICourseRepository> _mockCourseRepository;

        public GetCoursesListQueryHandlerTest()
        {
            _mockCourseRepository = CourseRepositoryMock.GetCourseRepository();
        }

        [Fact]
        public async Task GetCourseList_ReturnsListOfCourses()
        {
            var handler = new GetCoursesListQueryHandler(_mapper, _mockCourseRepository.Object);

            var result = await handler.Handle(new GetCoursesListQuery(), CancellationToken.None);

            result.ShouldBeOfType<List<CourseListVm>>();

            result.Count.ShouldBe(4);
        }

        [Fact]
        public async Task GetOnlyPublishedCourseList_ReturnsListOfCourses()
        {
            var handler = new GetCoursesListQueryHandler(_mapper, _mockCourseRepository.Object);

            var result = await handler.Handle(new GetCoursesListQuery() { OnlyPublished = true }, CancellationToken.None);

            result.ShouldBeOfType<List<CourseListVm>>();

            result.Count.ShouldBe(2);
        }
    }
}
