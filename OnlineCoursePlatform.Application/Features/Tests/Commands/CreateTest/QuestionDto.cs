namespace OnlineCoursePlatform.Application.Features.Tests.Commands.CreateTest
{
    public class QuestionDto
    {
        public string Text { get; set; } = string.Empty;

        public List<AnswerDto> Answers { get; set; } = default!;
    }
}
