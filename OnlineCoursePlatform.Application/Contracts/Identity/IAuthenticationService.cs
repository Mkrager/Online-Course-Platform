using OnlineCoursePlatform.Application.DTOs.Authentication;

namespace OnlineCoursePlatform.Application.Contracts.Identity
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request);
        Task<string> RegisterAsync(RegistrationRequest request);
    }
}
