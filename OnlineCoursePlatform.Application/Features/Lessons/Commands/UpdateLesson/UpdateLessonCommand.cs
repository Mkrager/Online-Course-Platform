using MediatR;

namespace OnlineCoursePlatform.Application.Features.Lessons.Commands.UpdateLesson
{
    public class UpdateLessonCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string VideoUrl { get; set; } = string.Empty;
        public int Order { get; set; }
        public TimeSpan Duration { get; set; }

    }
}
