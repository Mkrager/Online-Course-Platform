namespace OnlineCoursePlatform.Application.Features.Tests.Queries.GetTestDetail
{
    public class AnswerDetailDto
    {
        public Guid Id { get; set; }
        public Guid QuestionId { get; set; }
        public string Text { get; set; } = string.Empty;
        public bool IsCorrect { get; set; }
    }
}
