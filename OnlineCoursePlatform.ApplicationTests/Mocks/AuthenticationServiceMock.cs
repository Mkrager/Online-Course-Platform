using Moq;
using OnlineCoursePlatform.Application.Contracts.Identity;
using OnlineCoursePlatform.Application.DTOs.Authentication;

namespace OnlineCoursePlatform.Application.UnitTests.Mocks
{
    public class AuthenticationServiceMock
    {
        public static Mock<IAuthenticationService> GetAuthenticationService()
        {
            var mockService = new Mock<IAuthenticationService>();

            mockService.Setup(service => service.AuthenticateAsync(It.IsAny<AuthenticationRequest>()))
                .ReturnsAsync(new AuthenticationResponse
                {
                    Token = "fake-token",
                    Email = "fake-email",
                    Id = "fake-id",
                    UserName = "fake-userName"
                });


            mockService.Setup(service => service.RegisterAsync(It.IsAny<RegistrationRequest>()))
                .ReturnsAsync("some-id");

            return mockService;
        }
    }
}
