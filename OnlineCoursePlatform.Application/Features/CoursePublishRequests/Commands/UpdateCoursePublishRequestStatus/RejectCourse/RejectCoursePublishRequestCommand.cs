using MediatR;

namespace OnlineCoursePlatform.Application.Features.CoursePublishRequests.Commands.UpdateCoursePublishRequestStatus.RejectCourse
{
    public class RejectCoursePublishRequestCommand : IRequest
    {
        public Guid Id { get; set; }
        public string RejectReason { get; set; } = string.Empty;
    }
}
