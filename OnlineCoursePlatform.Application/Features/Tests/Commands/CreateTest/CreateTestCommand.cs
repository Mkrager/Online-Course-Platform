using MediatR;

namespace OnlineCoursePlatform.Application.Features.Tests.Commands.CreateTest
{
    public class CreateTestCommand : IRequest<Guid>
    {
        public Guid LessonId { get; set; }
        public string Title { get; set; } = string.Empty;

        public List<QuestionDto> Questions { get; set; } = default!;
    }
}
