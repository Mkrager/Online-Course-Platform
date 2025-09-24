using MediatR;

namespace OnlineCoursePlatform.Application.Features.Courses.Commands.UnPublishCourse
{
    public class UnPublishCourseCommand : IRequest
    {
        public Guid Id { get; set; }
        public string UserId { get; set; } = string.Empty;
    }
}
