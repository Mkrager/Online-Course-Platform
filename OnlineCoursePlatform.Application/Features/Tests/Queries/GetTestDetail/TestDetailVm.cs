namespace OnlineCoursePlatform.Application.Features.Tests.Queries.GetTestDetail
{
    public class TestDetailVm
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public Guid LessonId { get; set; }
        public List<QuestionDetailDto> Questions { get; set; } = new();
    }
}
