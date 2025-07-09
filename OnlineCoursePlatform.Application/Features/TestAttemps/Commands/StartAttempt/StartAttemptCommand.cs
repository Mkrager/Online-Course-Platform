using MediatR;

namespace OnlineCoursePlatform.Application.Features.TestAttemps.Commands.StartAttempt
{
    public class StartAttemptCommand : IRequest<Guid>
    {
        public string UserId { get; set; } = string.Empty;
        public Guid TestId { get; set; }
    }
}
