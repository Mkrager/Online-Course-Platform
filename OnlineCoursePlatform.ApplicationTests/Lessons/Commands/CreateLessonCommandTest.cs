using AutoMapper;
using Moq;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Features.Courses.Commands.CreateCourse;
using OnlineCoursePlatform.Application.Features.Lessons.Commands.CreateLesson;
using OnlineCoursePlatform.Application.Profiles;
using OnlineCoursePlatform.Application.UnitTests.Mocks;
using Shouldly;

namespace OnlineCoursePlatform.Application.UnitTests.Lessons.Commands
{
    public class CreateLessonCommandTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<ILessonRepository> _mockLessonRepository;

        public CreateLessonCommandTest()
        {
            _mockLessonRepository = RepositoryMocks.GetLessonRepository();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task Should_Create_Lesson_Successfully()
        {
            var handler = new CreateLessonCommandHandler(_mockLessonRepository.Object, _mapper);

            var courseId = Guid.NewGuid();

            var command = new CreateLessonCommand
            {
                Description = "CreatedTestCreatedTestCreatedTestCreatedTestCreatedTestCreatedTestCreatedTest",
                Title = "CreatedTest",
                CourseId = courseId,
                Order = 1,
            };

            await handler.Handle(command, CancellationToken.None);

            var allLessons = await _mockLessonRepository.Object.ListAllAsync();
            allLessons.Count.ShouldBe(3);

            var createdLesson = allLessons.FirstOrDefault(a => a.Title == command.Title && a.Description == command.Description);
            createdLesson.ShouldNotBeNull();
            createdLesson.Title.ShouldBe(command.Title);
            createdLesson.Description.ShouldBe(command.Description);
            createdLesson.CourseId.ShouldBe(courseId);
        }

        [Fact]
        public async void Validator_ShouldHaveError_WhenTitleGratherThan100()
        {
            var validator = new CreateLessonCommandValidator();
            var query = new CreateLessonCommand
            {
                CourseId = Guid.Parse("c3e5468c-a2f9-4853-8ac1-5f8d098dd2c8"),
                Order = 1,
                Description = "Description",
                Title = "TitleTitleTitleTitleTitleTitleTitleTitleTitleTitleTitleTitleTitleTitleTitleTitleTitleTitleTitleTitleTitle",
                VideoUrl = "videoUrl"
            };

            var result = await validator.ValidateAsync(query);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, f => f.PropertyName == "Title");
        }

        [Fact]
        public async void Validator_ShouldHaveError_WhenOrderDontGratherThan0()
        {
            var validator = new CreateLessonCommandValidator();
            var query = new CreateLessonCommand
            {
                CourseId = Guid.Parse("c3e5468c-a2f9-4853-8ac1-5f8d098dd2c8"),
                Order = 0,
                Description = "Description",
                Title = "Title",
                VideoUrl = "videoUrl"
            };

            var result = await validator.ValidateAsync(query);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, f => f.PropertyName == "Order");
        }
    }
}
