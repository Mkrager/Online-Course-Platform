using MediatR;

namespace OnlineCoursePlatform.Application.Features.TestAttemps.Commands.StartAttempt
{
    public class StartAttemptCommand : IRequest<Guid>
    {
        public string UserId { get; set; } = string.Empty;
        public Guid TestId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public bool IsCompleted { get; set; }

        List<UserAnswerDto> UserAnswers { get; set; } = default!;
    }
}
