using MediatR;

namespace OnlineCoursePlatform.Application.Features.TeacherApplications.Commands.UpdateCoursePublishRequestStatus.ApproveTeacher
{
    public class ApproveTeacherApplicationCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
