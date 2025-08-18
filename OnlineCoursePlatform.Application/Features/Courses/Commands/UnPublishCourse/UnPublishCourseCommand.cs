using MediatR;

namespace OnlineCoursePlatform.Application.Features.Courses.Commands.UnPublishCourse
{
    public class UnPublishCourseCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
