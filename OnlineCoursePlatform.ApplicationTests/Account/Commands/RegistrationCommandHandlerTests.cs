using AutoMapper;
using Moq;
using OnlineCoursePlatform.Application.Contracts.Identity;
using OnlineCoursePlatform.Application.Features.Account.Commands.Registration;
using OnlineCoursePlatform.Application.Profiles;

namespace OnlineCoursePlatform.Application.UnitTests.Account.Commands
{
    public class RegistrationCommandHandlerTests
    {
        private readonly Mock<IAuthenticationService> _mockAuthenticationService;
        private readonly IMapper _mapper;

        public RegistrationCommandHandlerTests()
        {
            _mockAuthenticationService = Mocks.RepositoryMocks.GetAuthenticationService();

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task Handle_WithValidValues_ReturnsUserId()
        {
            var handler = new RegistrationCommandHandler(_mockAuthenticationService.Object, _mapper);

            var registrationQuery = new RegistrationCommand()
            {
                Email = "email@gmail.com",
                Password = "Password132!",
                FirstName = "testName",
                LastName = "testLast",
                UserName = "testUsername"
            };

            var result = await handler.Handle(registrationQuery, CancellationToken.None);

            Assert.NotNull(result);
            Assert.IsType<string>(result);
        }

        [Fact]
        public async void Validator_ShouldHaveError_WhenEmailEmpty()
        {
            var validator = new RegistrationCommandValidator();
            var query = new RegistrationCommand
            {
                Email = "",
                Password = "password",
                FirstName = "testName",
                LastName = "testLast",
                UserName = "testUsername"
            };

            var result = await validator.ValidateAsync(query);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, f => f.PropertyName == "Email");
        }

        [Fact]
        public async void Validator_ShouldHaveError_WhenPasswordDontHaveNumber()
        {
            var validator = new RegistrationCommandValidator();
            var query = new RegistrationCommand
            {
                Email = "email@gmail.com",
                Password = "Password$",
                FirstName = "testName",
                LastName = "testLast",
                UserName = "testUsername"
            };

            var result = await validator.ValidateAsync(query);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, f => f.PropertyName == "Password");
        }

        [Fact]
        public async void Validator_ShouldHaveError_WhenPasswordDontHaveSpecialCharacter()
        {
            var validator = new RegistrationCommandValidator();
            var query = new RegistrationCommand
            {
                Email = "email@gmail.com",
                Password = "Password123",
                FirstName = "testName",
                LastName = "testLast",
                UserName = "testUsername"
            };

            var result = await validator.ValidateAsync(query);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, f => f.PropertyName == "Password");
        }

        [Fact]
        public async void Validator_ShouldHaveError_WhenPasswordDontHaveUpperCaseCharacter()
        {
            var validator = new RegistrationCommandValidator();
            var query = new RegistrationCommand
            {
                Email = "email@gmail.com",
                Password = "password123$",
                FirstName = "testName",
                LastName = "testLast",
                UserName = "testUsername"
            };

            var result = await validator.ValidateAsync(query);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, f => f.PropertyName == "Password");
        }

        [Fact]
        public async void Validator_ShouldDontHaveError_WhenPasswordCorrect()
        {
            var validator = new RegistrationCommandValidator();
            var query = new RegistrationCommand
            {
                Email = "email@gmail.com",
                Password = "Password123$",
                FirstName = "testName",
                LastName = "testLast",
                UserName = "testUsername"
            };

            var result = await validator.ValidateAsync(query);

            Assert.True(result.IsValid);
        }
    }
}
