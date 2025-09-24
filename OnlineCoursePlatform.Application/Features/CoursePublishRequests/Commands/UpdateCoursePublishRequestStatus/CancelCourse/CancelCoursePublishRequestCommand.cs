using MediatR;

namespace OnlineCoursePlatform.Application.Features.CoursePublishRequests.Commands.UpdateCoursePublishRequestStatus.CancelCourse
{
    public class CancelCoursePublishRequestCommand : IRequest
    {
        public Guid Id { get; set; }
        public string UserId { get; set; } = string.Empty;
    }
}
