using MediatR;

namespace OnlineCoursePlatform.Application.Features.TeacherApplications.Commands.UpdateTeacherApplicationStatus.RejectTeacher
{
    public class RejectTeacherApplicationCommand : IRequest
    {
        public Guid Id { get; set; }
        public string RejectReason { get; set; } = string.Empty;
    }
}
