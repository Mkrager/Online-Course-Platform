using MediatR;

namespace OnlineCoursePlatform.Application.Features.User.Commands.AssignRole.AssignTeacherRole
{
    public class AssignTeacherRoleCommand : IRequest
    {
        public string UserId { get; set; } = string.Empty;
    }
}
