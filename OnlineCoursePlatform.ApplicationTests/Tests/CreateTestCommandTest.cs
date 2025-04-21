using AutoMapper;
using Moq;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Features.Courses.Commands.CreateCourse;
using OnlineCoursePlatform.Application.Features.Tests.Commands.CreateTest;
using OnlineCoursePlatform.Application.Profiles;
using OnlineCoursePlatform.ApplicationTests.Mocks;
using OnlineCoursePlatform.Domain.Entities;
using Shouldly;

namespace OnlineCoursePlatform.Application.UnitTests.Tests
{
    public class CreateTestCommandTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IAsyncRepository<Test>> _mockTestRepository;

        public CreateTestCommandTest()
        {
            _mockTestRepository = RepositoryMocks.GetTestRepository();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task Should_Create_Test_Successfully()
        {
            var handler = new CreateTestCommandHandler(_mapper, _mockTestRepository.Object);

            var command = new CreateTestCommand
            {
                Title = "CreatedTest",
                CourseId = Guid.NewGuid(),
            };

            await handler.Handle(command, CancellationToken.None);

            var allCourses = await _mockTestRepository.Object.ListAllAsync();
            allCourses.Count.ShouldBe(3);

            var createdCourse = allCourses.FirstOrDefault(a => a.Title == command.Title);
            createdCourse.ShouldNotBeNull();
            createdCourse.Title.ShouldBe(command.Title);
        }

    }
}
