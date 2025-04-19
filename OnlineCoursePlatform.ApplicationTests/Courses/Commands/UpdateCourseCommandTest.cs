using AutoMapper;
using Moq;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Features.Courses.Commands.UpdateCourse;
using OnlineCoursePlatform.Application.Profiles;
using OnlineCoursePlatform.ApplicationTests.Mocks;
using OnlineCoursePlatform.Domain.Entities;
using Shouldly;

namespace OnlineCoursePlatform.ApplicationTests.Courses.Commands
{
    public class UpdateCourseCommandTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IAsyncRepository<Course>> _mockCourseRepository;

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

            var updatedAccommodation = await _mockCourseRepository.Object.GetByIdAsync(updateCommand.Id);

            updatedAccommodation.ShouldNotBeNull();
            updatedAccommodation.Title.ShouldBe(updateCommand.Title);
            updatedAccommodation.Description.ShouldBe(updateCommand.Description);
            updatedAccommodation.CategoryId.ShouldBe(updateCommand.CategoryId);
            updatedAccommodation.ThumbnailUrl.ShouldBe(updateCommand.ThumbnailUrl);
            updatedAccommodation.Price.ShouldBe(updateCommand.Price);
        }

    }
}
