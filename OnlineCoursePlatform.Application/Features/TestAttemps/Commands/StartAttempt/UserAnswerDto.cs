namespace OnlineCoursePlatform.Application.Features.TestAttemps.Commands.StartAttempt
{
    public class UserAnswerDto
    {
        public Guid QuestionId { get; set; }
        public Guid AnswerId { get; set; }
        public string UserId { get; set; } = string.Empty;
        public Guid TestAttemptId { get; set; }
        public bool IsCorrect { get; set; }
    }
}
