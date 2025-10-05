using MediatR;

namespace OnlineCoursePlatform.Application.Features.TeacherApplications.Commands.UpdateTeacherApplicationStatus.ApproveTeacher
{
    public class ApproveTeacherApplicationCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
