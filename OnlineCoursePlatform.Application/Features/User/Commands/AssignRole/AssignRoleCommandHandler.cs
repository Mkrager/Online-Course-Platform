using MediatR;
using OnlineCoursePlatform.Application.Contracts.Identity;

namespace OnlineCoursePlatform.Application.Features.User.Commands.AssignRole
{
    public class AssignRoleCommandHandler : IRequestHandler<AssignRoleCommand>
    {
        private readonly IUserService _userService;

        public AssignRoleCommandHandler(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<Unit> Handle(AssignRoleCommand request, CancellationToken cancellationToken)
        {
            await _userService.AssignRoleAsync(request.UserId, request.RoleName);

            return Unit.Value;
        }
    }
}
