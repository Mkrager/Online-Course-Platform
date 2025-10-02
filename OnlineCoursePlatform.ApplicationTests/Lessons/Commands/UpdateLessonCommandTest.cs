using Moq;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Features.Lessons.Commands.UpdateLesson;
using OnlineCoursePlatform.Application.UnitTests.Base;
using OnlineCoursePlatform.Application.UnitTests.Mocks;
using Shouldly;

namespace OnlineCoursePlatform.Application.UnitTests.Lessons.Commands
{
    public class UpdateLessonCommandTest : AccessValidatorBaseTest
    {
        private readonly Mock<ILessonRepository> _mockLessonRepository;

        public UpdateLessonCommandTest()
        {
            _mockLessonRepository = LessonRepositoryMock.GetLessonRepository();
        }

        [Fact]
        public async Task UpdateLesson_ValidCommand_UpdatesLessonSuccessfully()
        {
            var handler = new UpdateLessonCommandHandler(_mockLessonRepository.Object, _mapper);

            var updateLessonCommand = new UpdateLessonCommand
            {
                Id = Guid.Parse("b8c3f27a-7b28-4ae6-94c2-91fdc33b77e8"),
                Description = "updDescriptionupdDescriptionupdDescriptionupdDescriptionupdDescriptionupdDescriptionupdDescriptionupdDescriptionupdDescriptionupdDescriptionupdDescriptionupdDescriptionupdDescription",
                Order = 1,
                Title = "updTitle"
            };

            await handler.Handle(updateLessonCommand, CancellationToken.None);

            var updateLesson = await _mockLessonRepository.Object.GetByIdAsync(Guid.Parse("b8c3f27a-7b28-4ae6-94c2-91fdc33b77e8"));

            updateLesson.ShouldNotBeNull();
            updateLesson.Description.ShouldBe(updateLessonCommand.Description);
            updateLesson.Id.ShouldBe(updateLessonCommand.Id);
            updateLesson.Title.ShouldBe(updateLessonCommand.Title);
            updateLesson.Order.ShouldBe(updateLessonCommand.Order);
        }

        [Fact]
        public async void Validator_ShouldHaveError_WhenTitleEmpty()
        {
            var validator = new UpdateLessonCommandValidator(_mockCourseRepository.Object, _mockPermissionService.Object);
            var query = new UpdateLessonCommand
            {
                Id = Guid.Parse("b8c3f27a-7b28-4ae6-94c2-91fdc33b77e8"),
                Order = 1,
                Description = "Description",
                Title = "",
                VideoUrl = "videoUrl"
            };

            var result = await validator.ValidateAsync(query);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, f => f.PropertyName == "Title");
        }

        [Fact]
        public async void Validator_ShouldHaveError_WhenTitleGratherThan100()
        {
            var validator = new UpdateLessonCommandValidator(_mockCourseRepository.Object, _mockPermissionService.Object);
            var query = new UpdateLessonCommand
            {
                Id = Guid.Parse("b8c3f27a-7b28-4ae6-94c2-91fdc33b77e8"),
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
            var validator = new UpdateLessonCommandValidator(_mockCourseRepository.Object, _mockPermissionService.Object);
            var query = new UpdateLessonCommand
            {
                Id = Guid.Parse("b8c3f27a-7b28-4ae6-94c2-91fdc33b77e8"),
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
