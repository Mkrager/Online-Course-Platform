namespace OnlineCoursePlatform.Application.Features.TestAttemps.Commands.EndAttempt
{
    public class UserAnswerDto
    {
        public Guid QuestionId { get; set; }
        public Guid AnswerId { get; set; }
        public string UserId { get; set; } = string.Empty;
        //public bool IsCorrect { get; set; }
    }
}
