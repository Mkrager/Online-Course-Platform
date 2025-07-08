using AutoMapper;
using Moq;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Features.Courses.Commands.CreateCourse;
using OnlineCoursePlatform.Application.Features.Courses.Commands.UpdateCourse;
using OnlineCoursePlatform.Application.Profiles;
using OnlineCoursePlatform.Application.UnitTests.Mocks;
using OnlineCoursePlatform.Domain.Entities;
using Shouldly;

namespace OnlineCoursePlatform.ApplicationTests.Courses.Commands
{
    public class UpdateCourseCommandTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<ICourseRepository> _mockCourseRepository;

        public UpdateCourseCommandTest()
        {
            _mockCourseRepository = RepositoryMocks.GetCourseRepository();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task UpdateCourse_ValidCommand_UpdatesCourseSuccessfully()
        {
            var handler = new UpdateCourseCommandHandler(_mapper, _mockCourseRepository.Object);
            var updateCommand = new UpdateCourseCommand
            {
                Id = Guid.Parse("b8c3f27a-7b28-4ae6-94c2-91fdc33b77e8"),
                Title = "Updated Test",
                CategoryId = Guid.Parse("c119661c-1d5a-42c1-8819-6b0885af4d4a"),
                Price = 2000,
                Description = "Updated TestUpdated TestUpdated TestUpdated TestUpdated TestUpdated TestUpdated TestUpdated TestUpdated TestUpdated Test",
                ThumbnailUrl = "test"
            };

            await handler.Handle(updateCommand, CancellationToken.None);

            var updatedCourse = await _mockCourseRepository.Object.GetByIdAsync(updateCommand.Id);

            updatedCourse.ShouldNotBeNull();
            updatedCourse.Title.ShouldBe(updateCommand.Title);
            updatedCourse.Description.ShouldBe(updateCommand.Description);
            updatedCourse.CategoryId.ShouldBe(updateCommand.CategoryId);
            updatedCourse.ThumbnailUrl.ShouldBe(updateCommand.ThumbnailUrl);
            updatedCourse.Price.ShouldBe(updateCommand.Price);
        }

        [Fact]
        public async void Validator_ShouldHaveError_WhenTitleEmpty()
        {
            var validator = new UpdateCourseCommandValidator();
            var query = new UpdateCourseCommand
            {
                Id = Guid.Parse("3f2a3a3e-27c9-4b65-bfb4-2b1e3d4b54ee"),
                CategoryId = Guid.Parse("7d4f7640-21b2-46c4-a311-0f426812386b"),
                Description = "DescriptionDescriptionDescriptionDescriptionDescriptionDescription",
                Price = 100,
                ThumbnailUrl = "url",
                Title = ""
            };

            var result = await validator.ValidateAsync(query);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, f => f.PropertyName == "Title");
        }

        [Fact]
        public async void Validator_ShouldHaveError_WhenPriceDontGrather0()
        {
            var validator = new UpdateCourseCommandValidator();
            var query = new UpdateCourseCommand
            {
                Id = Guid.Parse("3f2a3a3e-27c9-4b65-bfb4-2b1e3d4b54ee"),
                CategoryId = Guid.Parse("7d4f7640-21b2-46c4-a311-0f426812386b"),
                Description = "DescriptionDescriptionDescriptionDescriptionDescriptionDescription",
                Price = 0,
                ThumbnailUrl = "url",
                Title = "Title"
            };

            var result = await validator.ValidateAsync(query);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, f => f.PropertyName == "Price");
        }

        [Fact]
        public async void Validator_ShouldHaveError_WhenDescriptionDontGrather50Characters()
        {
            var validator = new UpdateCourseCommandValidator();
            var query = new UpdateCourseCommand
            {
                Id = Guid.Parse("3f2a3a3e-27c9-4b65-bfb4-2b1e3d4b54ee"),
                CategoryId = Guid.Parse("7d4f7640-21b2-46c4-a311-0f426812386b"),
                Description = "Description",
                Price = 100,
                ThumbnailUrl = "url",
                Title = "Title"
            };

            var result = await validator.ValidateAsync(query);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, f => f.PropertyName == "Description");
        }

        [Fact]
        public async void Validator_ShouldHaveError_WhenTitleGrather100Characters()
        {
            var validator = new UpdateCourseCommandValidator();
            var query = new UpdateCourseCommand
            {
                Id = Guid.Parse("3f2a3a3e-27c9-4b65-bfb4-2b1e3d4b54ee"),
                CategoryId = Guid.Parse("7d4f7640-21b2-46c4-a311-0f426812386b"),
                Description = "DescriptionDescriptionDescriptionDescriptionDescriptionDescription",
                Price = 100,
                ThumbnailUrl = "url",
                Title = "TitleTitleTitleTitleTitleTitleTitleTitleTitleTitleTitleTitleTitleTitleTitleTitleTitleTitleTitleTitleTitle"
            };

            var result = await validator.ValidateAsync(query);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, f => f.PropertyName == "Title");
        }
    }
}
