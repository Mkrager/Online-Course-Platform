using MediatR;

namespace OnlineCoursePlatform.Application.Features.CoursePublishRequests.Commands.CancelCourse
{
    public class CancelCoursePublishRequestCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
