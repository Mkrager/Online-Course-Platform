using MediatR;

namespace OnlineCoursePlatform.Application.Features.TeacherApplications.Commands.UpdateCoursePublishRequestStatus.RejectTeacher
{
    public class RejectCoursePublishRequestCommand : IRequest
    {
        public Guid Id { get; set; }
        public string RejectReason { get; set; } = string.Empty;
    }
}
