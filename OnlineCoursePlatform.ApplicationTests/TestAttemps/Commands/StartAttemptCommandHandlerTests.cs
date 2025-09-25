using Moq;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Features.TestAttemps.Commands.StartAttempt;
using OnlineCoursePlatform.Application.UnitTests.Base;
using OnlineCoursePlatform.Application.UnitTests.Mocks;
using OnlineCoursePlatform.Domain.Entities;
using Shouldly;

namespace OnlineCoursePlatform.Application.UnitTests.TestAttemps.Commands
{
    public class StartAttemptCommandHandlerTests : AccessValidatorBaseTest
    {
        private readonly Mock<IAsyncRepository<TestAttempt>> _mockTestAttemptRepository;

        public StartAttemptCommandHandlerTests()
        {
            _mockTestAttemptRepository = TestAttemptRepositoryMock.GetTestAttemptRepository();
        }

        [Fact]
        public async Task Should_Add_TestAttempt_Successfully()
        {
            var handler = new StartAttemptCommandHandler(_mapper, _mockTestAttemptRepository.Object);

            var command = new StartAttemptCommand
            {
                TestId = Guid.NewGuid(),
            };

            await handler.Handle(command, CancellationToken.None);

            var allAttempt = await _mockTestAttemptRepository.Object.ListAllAsync();
            allAttempt.Count.ShouldBe(3);

            var createdAttempt = allAttempt.FirstOrDefault(a => a.TestId == command.TestId);
            createdAttempt.ShouldNotBeNull();
            createdAttempt.TestId.ShouldBe(command.TestId);
            createdAttempt.StartTime.ShouldBeInRange(createdAttempt.StartTime - TimeSpan.FromSeconds(1), createdAttempt.StartTime + TimeSpan.FromSeconds(1));
        }

        [Fact]
        public async Task Validator_ShouldHaveError_WhenTestIdEmpty()
        {
            var validator = new StartAttemptCommandValidator(_mockCourseRepository.Object, _mockPermissionService.Object);
            var query = new StartAttemptCommand()
            {
                TestId = Guid.Empty,
            };

            var result = await validator.ValidateAsync(query);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, f => f.PropertyName == "TestId");
        }
    }
}
