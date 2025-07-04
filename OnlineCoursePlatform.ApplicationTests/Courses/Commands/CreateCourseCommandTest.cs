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
    }
}
