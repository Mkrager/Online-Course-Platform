using MediatR;

namespace OnlineCoursePlatform.Application.Features.User.Commands.AssignRole
{
    public class AssignRoleCommand : IRequest
    {
        public string UserId { get; set; } = string.Empty;
        public string RoleName { get; set; } = string.Empty;
    }
}
