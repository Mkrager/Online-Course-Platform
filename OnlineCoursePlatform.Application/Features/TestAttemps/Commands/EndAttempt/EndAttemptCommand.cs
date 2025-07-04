using MediatR;

namespace OnlineCoursePlatform.Application.Features.TestAttemps.Commands.EndAttempt
{
    public class EndAttemptCommand : IRequest
    {
        public Guid AttempId { get; set; }
        public List<UserAnswerDto> UserAnswerDto { get; set; } = default!;
    }
}
