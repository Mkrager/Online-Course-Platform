using OnlineCoursePlatform.Application.Features.Tests.Commands.CreateTest;

namespace OnlineCoursePlatform.Application.Features.Tests.Queries.GetTestDetail
{
    public class QuestionDetailDto
    {
        public Guid Id { get; set; }
        public Guid TestId { get; set; }
        public string Text { get; set; } = string.Empty;

        public List<AnswerDetailDto> Answers { get; set; } = new();
    }
}
