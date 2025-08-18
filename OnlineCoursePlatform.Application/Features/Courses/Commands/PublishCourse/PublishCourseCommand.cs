using MediatR;

namespace OnlineCoursePlatform.Application.Features.Courses.Commands.PublishCourse
{
    public class PublishCourseCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
