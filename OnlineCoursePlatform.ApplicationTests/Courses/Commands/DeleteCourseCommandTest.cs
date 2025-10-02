using Moq;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Features.Courses.Commands.DeleteCourse;
using OnlineCoursePlatform.Application.UnitTests.Base;
using OnlineCoursePlatform.Application.UnitTests.Mocks;
using Shouldly;

namespace OnlineCoursePlatform.ApplicationTests.Courses.Commands
{
    public class DeleteCourseCommandTest
    {
        private readonly Mock<ICourseRepository> _mockCourseRepository;

        public DeleteCourseCommandTest()
        {
            _mockCourseRepository = CourseRepositoryMock.GetCourseRepository();
        }

        [Fact]
        public async Task Delete_Course_RemovesCourseFromRepo()
        {
            var handler = new DeleteCourseCommandHandler(_mockCourseRepository.Object);
            await handler.Handle(new DeleteCourseCommand() { Id = Guid.Parse("b8c3f27a-7b28-4ae6-94c2-91fdc33b77e8") }, CancellationToken.None);

            var allCourses = await _mockCourseRepository.Object.ListAllAsync();
            allCourses.Count.ShouldBe(3);
        }
    }
}