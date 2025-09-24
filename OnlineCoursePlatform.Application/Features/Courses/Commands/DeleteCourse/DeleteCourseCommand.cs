using MediatR;

namespace OnlineCoursePlatform.Application.Features.Courses.Commands.DeleteCourse
{
    public class DeleteCourseCommand : IRequest
    {
        public Guid Id { get; set; }
        public string UserId { get; set; } = string.Empty;
    }
}
