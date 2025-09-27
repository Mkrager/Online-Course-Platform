using FluentValidation;

namespace OnlineCoursePlatform.Application.Features.User.Commands.AssignRole.AssignTeacherRole
{
    public class AssignTeacherRoleCommandValidator : AbstractValidator<AssignTeacherRoleCommand>
    {
        public AssignTeacherRoleCommandValidator()
        {
            RuleFor(r => r.UserId)
                .NotEmpty().WithMessage("UserId must not be empty");
        }
    }
}
