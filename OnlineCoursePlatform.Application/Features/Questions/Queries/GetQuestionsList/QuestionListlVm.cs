namespace OnlineCoursePlatform.Application.Features.Questions.Queries.GetQuestionList
{
    public class QuestionListlVm
    {
        public Guid Id { get; set; }
        public Guid TestId { get; set; }
        public string Text { get; set; } = string.Empty;
    }
}
