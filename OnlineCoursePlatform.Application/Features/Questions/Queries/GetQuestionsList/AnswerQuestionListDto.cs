namespace OnlineCoursePlatform.Application.Features.Questions.Queries.GetQuestionList
{
    public class AnswerQuestionListDto
    {
        public Guid Id { get; set; }
        public Guid QuestionId { get; set; }
        public string Text { get; set; } = string.Empty;
        public bool IsCorrect { get; set; }
    }
}
