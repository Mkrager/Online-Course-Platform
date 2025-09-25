using Moq;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Features.Courses.Queries.GetCoursesByTeacher;
using OnlineCoursePlatform.Application.UnitTests.Base;
using OnlineCoursePlatform.Application.UnitTests.Mocks;
using Shouldly;

namespace OnlineCoursePlatform.Application.UnitTests.Courses.Queries
{
    public class GetCoursesByTeacherQueryHandlerTests : TestBase
    {
        private readonly Mock<ICourseRepository> _mockCourseRepository;

        public GetCoursesByTeacherQueryHandlerTests()
        {
            _mockCourseRepository = CourseRepositoryMock.GetCourseRepository();
        }

        [Fact]
        public async Task GetTeacherCourseList_ReturnsListOfTeacherCourses()
        {
            var handler = new GetCoursesByTeacherQueryHandler(_mapper, _mockCourseRepository.Object);

            var result = await handler.Handle(new GetCoursesByTeacherQuery() { UserId = "id" }, CancellationToken.None);

            result.ShouldBeOfType<List<TeacherCourseDetailVm>>();

            result.Count.ShouldBe(2);
        }

    }
}
