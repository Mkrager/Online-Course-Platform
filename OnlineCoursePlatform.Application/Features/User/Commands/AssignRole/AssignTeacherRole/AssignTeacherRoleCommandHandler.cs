using MediatR;
using OnlineCoursePlatform.Application.Contracts.Identity;

namespace OnlineCoursePlatform.Application.Features.User.Commands.AssignRole.AssignTeacherRole
{
    public class AssignTeacherRoleCommandHandler : IRequestHandler<AssignTeacherRoleCommand>
    {
        private readonly IUserService _userService;

        public AssignTeacherRoleCommandHandler(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<Unit> Handle(AssignTeacherRoleCommand request, CancellationToken cancellationToken)
        {
            await _userService.AssignRoleAsync(request.UserId, "Teacher");

            return Unit.Value;
        }
    }
}