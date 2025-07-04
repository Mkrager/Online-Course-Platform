using AutoMapper;
using Moq;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Features.TestAttemps.Commands.StartAttempt;
using OnlineCoursePlatform.Application.Profiles;
using OnlineCoursePlatform.Application.UnitTests.Mocks;
using OnlineCoursePlatform.Domain.Entities;
using Shouldly;

namespace OnlineCoursePlatform.Application.UnitTests.TestAttemps.Commands
{
    public class StartAttemptCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IAsyncRepository<TestAttempt>> _mockTestAttemptRepository;

        public StartAttemptCommandHandlerTests()
        {
            _mockTestAttemptRepository = RepositoryMocks.GetTestAttemptRepository();

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task Should_Add_TestAttempt_Successfully()
        {
            var handler = new StartAttemptCommandHandler(_mapper, _mockTestAttemptRepository.Object);

            var command = new StartAttemptCommand
            {
                IsCompleted = false,
                TestId = Guid.NewGuid(),
                StartTime = DateTime.UtcNow,
                UserId = "newUserId"
            };

            await handler.Handle(command, CancellationToken.None);

            var allCourses = await _mockTestAttemptRepository.Object.ListAllAsync();
            allCourses.Count.ShouldBe(3);

            var createdCourse = allCourses.FirstOrDefault(a => a.UserId == command.UserId && a.UserId == command.UserId);
            createdCourse.ShouldNotBeNull();
            createdCourse.IsCompleted.ShouldBe(command.IsCompleted);
            createdCourse.TestId.ShouldBe(command.TestId);
            createdCourse.StartTime.ShouldBeInRange(command.StartTime - TimeSpan.FromSeconds(1), command.StartTime + TimeSpan.FromSeconds(1)); 
            createdCourse.UserId.ShouldBe(command.UserId);
        }
    }
}
