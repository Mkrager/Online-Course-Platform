using AutoMapper;
using Moq;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Features.TestAttemps.Commands.EndAttempt;
using OnlineCoursePlatform.Application.Features.TestAttemps.Commands.StartAttempt;
using OnlineCoursePlatform.Application.Profiles;
using OnlineCoursePlatform.Domain.Entities;
using Shouldly;

namespace OnlineCoursePlatform.Application.UnitTests.TestAttemps.Commands
{
    public class EndAttemptCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IAsyncRepository<TestAttempt>> _mockTestAttemptRepository;
        private readonly Mock<IUserAnswerRepository> _mockUserAnswerRepository;

        public EndAttemptCommandHandlerTests()
        {
            _mockTestAttemptRepository = Mocks.RepositoryMocks.GetTestAttemptRepository();
            _mockUserAnswerRepository = Mocks.RepositoryMocks.GetUserAnswerRepository();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task Should_Update_TestAttempt_Successfully()
        {
            var handler = new EndAttemptCommandHandler(_mapper, _mockTestAttemptRepository.Object, _mockUserAnswerRepository.Object);

            var command = new EndAttemptCommand
            {
                AttempId = Guid.Parse("9f2b5c3e-6d3e-4c9f-9b57-3a8c4f0b1207"),
                UserAnswerDto = new List<UserAnswerDto>
                {
                    new UserAnswerDto()
                    {
                        IsCorrect = true,
                    }
                }
            };

            await handler.Handle(command, CancellationToken.None);

            var updatedTestAttempt = await _mockTestAttemptRepository.Object.GetByIdAsync(command.AttempId);

            updatedTestAttempt.ShouldNotBeNull();
            updatedTestAttempt.Id.ShouldBe(command.AttempId);
            updatedTestAttempt.UserAnswers.ShouldNotBeNull();
        }
    }
}
