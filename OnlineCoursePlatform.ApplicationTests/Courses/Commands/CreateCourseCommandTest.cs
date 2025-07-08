using AutoMapper;
using Moq;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Features.Courses.Commands.CreateCourse;
using OnlineCoursePlatform.Application.Profiles;
using OnlineCoursePlatform.Application.UnitTests.Mocks;
using Shouldly;

namespace OnlineCoursePlatform.ApplicationTests.Courses.Commands
{
    public class CreateCourseCommandTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<ICourseRepository> _mockCourseRepository;

        public CreateCourseCommandTest()
        {
            _mockCourseRepository = RepositoryMocks.GetCourseRepository();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task Should_Create_Course_Successfully()
        {
            var handler = new CreateCourseCommandHandler(_mapper, _mockCourseRepository.Object);

            var command = new CreateCourseCommand
            {
                ThumbnailUrl = "CreatedTest",
                Description = "CreatedTestCreatedTestCreatedTestCreatedTestCreatedTestCreatedTestCreatedTest",
                Title = "CreatedTest",
                CategoryId = Guid.Parse("c66f1c3d-8749-42d0-97ee-3f50a7421d08"),
                Price = 1000
            };

            await handler.Handle(command, CancellationToken.None);

            var allCourses = await _mockCourseRepository.Object.ListAllAsync();
            allCourses.Count.ShouldBe(3);

            var createdCourse = allCourses.FirstOrDefault(a => a.Title == command.Title && a.Price == command.Price);
            createdCourse.ShouldNotBeNull();
            createdCourse.Title.ShouldBe(command.Title);
            createdCourse.ThumbnailUrl.ShouldBe(command.ThumbnailUrl);
            createdCourse.CategoryId.ShouldBe(command.CategoryId);
            createdCourse.Description.ShouldBe(command.Description);
            createdCourse.Price.ShouldBe(command.Price);
        }

        [Fact]
        public async void Validator_ShouldHaveError_WhenTitleEmpty()
        {
            var validator = new CreateCourseCommandValidator();
            var query = new CreateCourseCommand
            {
                CategoryId = Guid.Parse("7d4f7640-21b2-46c4-a311-0f426812386b"),
                Description = "DescriptionDescriptionDescriptionDescriptionDescriptionDescription",
                LevelId = Guid.Parse("d1a5eb43-8772-4ee1-9ab2-e8600f3624a1"),
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
            var validator = new CreateCourseCommandValidator();
            var query = new CreateCourseCommand
            {
                CategoryId = Guid.Parse("7d4f7640-21b2-46c4-a311-0f426812386b"),
                Description = "DescriptionDescriptionDescriptionDescriptionDescriptionDescription",
                LevelId = Guid.Parse("d1a5eb43-8772-4ee1-9ab2-e8600f3624a1"),
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
            var validator = new CreateCourseCommandValidator();
            var query = new CreateCourseCommand
            {
                CategoryId = Guid.Parse("7d4f7640-21b2-46c4-a311-0f426812386b"),
                Description = "Description",
                LevelId = Guid.Parse("d1a5eb43-8772-4ee1-9ab2-e8600f3624a1"),
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
            var validator = new CreateCourseCommandValidator();
            var query = new CreateCourseCommand
            {
                CategoryId = Guid.Parse("7d4f7640-21b2-46c4-a311-0f426812386b"),
                Description = "DescriptionDescriptionDescriptionDescriptionDescriptionDescription",
                LevelId = Guid.Parse("d1a5eb43-8772-4ee1-9ab2-e8600f3624a1"),
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
