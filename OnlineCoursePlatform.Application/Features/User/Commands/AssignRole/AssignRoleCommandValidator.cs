using FluentValidation;

namespace OnlineCoursePlatform.Application.Features.User.Commands.AssignRole
{
    public class AssignRoleCommandValidator : AbstractValidator<AssignRoleCommand>
    {
        public AssignRoleCommandValidator()
        {
            RuleFor(r => r.UserId)
                .NotEmpty().WithMessage("UserId must not be empty");

            RuleFor(r => r.RoleName)
                .NotEmpty().WithMessage("RoleName must not be empty");
        }
    }
}
