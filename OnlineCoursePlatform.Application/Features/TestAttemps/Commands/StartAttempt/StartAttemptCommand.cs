using MediatR;

namespace OnlineCoursePlatform.Application.Features.TestAttemps.Commands.StartAttempt
{
    public class StartAttemptCommand : IRequest<Guid>
    {
        public Guid TestId { get; set; }
        public string UserId { get; set; } = string.Empty;
    }
}
