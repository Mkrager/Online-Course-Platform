using AutoMapper;
using Moq;
using OnlineCoursePlatform.Application.Contracts.Identity;
using OnlineCoursePlatform.Application.DTOs.Authentication;
using OnlineCoursePlatform.Application.Features.Account.Queries.Authentication;
using OnlineCoursePlatform.Application.Profiles;

namespace OnlineCoursePlatform.Application.UnitTests.Account.Queries
{
    public class AuthenticationQueryHandlerTests
    {
        private readonly Mock<IAuthenticationService> _mockAuthenticationService;
        private readonly IMapper _mapper;

        public AuthenticationQueryHandlerTests()
        {
            _mockAuthenticationService = Mocks.RepositoryMocks.GetAuthenticationService();

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task Handle_WithValidCredentials_ReturnsAuthenticationResponse()
        {
            var handler = new AuthenticationQueryHandler(_mockAuthenticationService.Object, _mapper);

            var authenticationQuery = new AuthenticationQuery()
            {
                Password = "password",
                Email = "email@gmail.com"
            };

            var result = await handler.Handle(authenticationQuery, CancellationToken.None);

            Assert.NotNull(result);
            Assert.IsType<AuthenticationVm>(result);
        }
    }
}
