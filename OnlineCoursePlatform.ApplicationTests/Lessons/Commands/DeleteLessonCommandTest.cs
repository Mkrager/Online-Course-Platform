using AutoMapper;
using Moq;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Features.Lessons.Commands.DeleteLesson;
using OnlineCoursePlatform.Application.Profiles;
using OnlineCoursePlatform.Application.UnitTests.Mocks;
using Shouldly;

namespace OnlineCoursePlatform.Application.UnitTests.Lessons.Commands
{
    public class DeleteLessonCommandTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<ILessonRepository> _mockLessonRepository;
        public DeleteLessonCommandTest()
        {
            _mockLessonRepository = LessonRepositoryMock.GetLessonRepository();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task Should_Delete_Lesson_Successfully()
        {
            var handler = new DeleteLessonCommandHandler(_mapper, _mockLessonRepository.Object);

            var result = handler.Handle(new DeleteLessonCommand() { Id = Guid.Parse("b8c3f27a-7b28-4ae6-94c2-91fdc33b77e8") }, CancellationToken.None);

            var allLessons = await _mockLessonRepository.Object.ListAllAsync();

            allLessons.Count.ShouldBe(1);
        }
    }
}
