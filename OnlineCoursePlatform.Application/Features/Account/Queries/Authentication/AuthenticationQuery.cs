using MediatR;
using OnlineCoursePlatform.Application.DTOs.Authentication;

namespace OnlineCoursePlatform.Application.Features.Account.Queries.Authentication
{
    public class AuthenticationQuery : IRequest<AuthenticationResponse>
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
