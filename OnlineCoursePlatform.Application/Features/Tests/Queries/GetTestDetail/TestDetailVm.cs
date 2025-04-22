namespace OnlineCoursePlatform.Application.Features.Tests.Queries.GetTestDetail
{
    public class TestDetailVm
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public List<QuestionDetailDto> Questions { get; set; } = new();
    }
}
