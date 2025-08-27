using MediatR;

namespace OnlineCoursePlatform.Application.Features.CoursePublishRequests.Commands.ApproveCourse
{
    public class ApproveCoursePublishRequestCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
