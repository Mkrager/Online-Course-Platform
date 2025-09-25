using MediatR;
using OnlineCoursePlatform.Application.Common.Interfaces;

namespace OnlineCoursePlatform.Application.Features.TestAttemps.Commands.StartAttempt
{
    public class StartAttemptCommand : IRequest<Guid>, IUserRequest
    {
        public Guid TestId { get; set; }

        public string UserId { get; set; } = string.Empty;
        public string UserRoles { get; set; } = string.Empty;
    }
}
