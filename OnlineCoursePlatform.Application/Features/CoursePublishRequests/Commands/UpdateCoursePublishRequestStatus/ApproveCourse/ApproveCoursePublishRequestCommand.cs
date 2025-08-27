using MediatR;

namespace OnlineCoursePlatform.Application.Features.CoursePublishRequests.Commands.UpdateCoursePublishRequestStatus.ApproveCourse
{
    public class ApproveCoursePublishRequestCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
