using MediatR;

namespace OnlineCoursePlatform.Application.Features.Lessons.Commands.DeleteLesson
{
    public class DeleteLessonCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
