using Moq;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Features.Lessons.Queries.GetCourseLessonsList;
using OnlineCoursePlatform.Application.UnitTests.Base;
using OnlineCoursePlatform.Application.UnitTests.Mocks;
using Shouldly;

namespace OnlineCoursePlatform.Application.UnitTests.Lessons.Queries
{
    public class GetCourseLessonsQueryHandlerTest : TestBase
    {
        private readonly Mock<ILessonRepository> _mockLessonRepository;
        public GetCourseLessonsQueryHandlerTest()
        {
            _mockLessonRepository = LessonRepositoryMock.GetLessonRepository();
        }

        [Fact]
        public async Task GetCourseLessons_RetursnListOfLessons()
        {
            var handler = new GetCourseLessonsQueryHandler(_mockLessonRepository.Object, _mapper);

            var result = await handler.Handle(new GetCourseLessonsQuery() { CourseId = Guid.Parse("7c38fdb6-3e86-4bc2-9c8d-bb7a5e1d9b72") }, CancellationToken.None);

            result.ShouldBeOfType<List<CourseLessonListVm>>();

            result.Count.ShouldBe(1);
        }
    }
}
