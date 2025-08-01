using AutoMapper;
using Moq;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Features.TestAttemps.Commands.EndAttempt;
using OnlineCoursePlatform.Application.Profiles;
using OnlineCoursePlatform.Application.UnitTests.Mocks;
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
            _mockTestAttemptRepository = TestAttemptRepositoryMock.GetTestAttemptRepository();
            _mockUserAnswerRepository = UserAnswerRepositoryMock.GetUserAnswerRepository();
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
                    }
                }
            };

            await handler.Handle(command, CancellationToken.None);

            var updatedTestAttempt = await _mockTestAttemptRepository.Object.GetByIdAsync(command.AttempId);

            updatedTestAttempt.ShouldNotBeNull();
            updatedTestAttempt.Id.ShouldBe(command.AttempId);
            updatedTestAttempt.UserAnswers.ShouldNotBeNull();
        }

        [Fact]
        public async Task Validator_ShouldHaveError_WhenUserAnswerEmpty()
        {
            var validator = new EndAttemptCommandValidator();
            var query = new EndAttemptCommand()
            {
                AttempId = Guid.Parse("6a89d499-b6b5-41e3-9377-4b60fad2bb38")
            };

            var result = await validator.ValidateAsync(query);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, f => f.PropertyName == "UserAnswerDto");
        }

        [Fact]
        public async Task Validator_ShouldHaveError_WhenAttemptIdEmpty()
        {
            var validator = new EndAttemptCommandValidator();
            var query = new EndAttemptCommand()
            {
                AttempId = Guid.Empty,
                UserAnswerDto = new List<UserAnswerDto>()
                {
                    new UserAnswerDto()
                    {
                        AnswerId = Guid.Parse("c51522a2-3297-4052-987e-8f7458d920a2"),
                        QuestionId = Guid.Parse("03b8c36c-ad34-4834-896e-df534c5739b5"),
                        UserId = "someUserId"
                    }
                }

            };

            var result = await validator.ValidateAsync(query);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, f => f.PropertyName == "AttempId");
        }

        [Fact]
        public async Task Validator_ShouldHaveError_WhenUserAnswerHasEmptyValue()
        {
            var validator = new EndAttemptCommandValidator();
            var query = new EndAttemptCommand()
            {
                AttempId = Guid.Parse("6a89d499-b6b5-41e3-9377-4b60fad2bb38"),
                UserAnswerDto = new List<UserAnswerDto>()
                {
                    new UserAnswerDto()
                    {
                        AnswerId = Guid.Parse("c51522a2-3297-4052-987e-8f7458d920a2"),
                        QuestionId = Guid.Empty,
                        UserId = "someUserId"
                    }
                }

            };

            var result = await validator.ValidateAsync(query);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, f => f.PropertyName == "UserAnswerDto[0].QuestionId");
        }
    }
}
