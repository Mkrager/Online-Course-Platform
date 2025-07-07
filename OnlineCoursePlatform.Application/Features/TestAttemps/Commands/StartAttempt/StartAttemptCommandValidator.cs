using FluentValidation;

namespace OnlineCoursePlatform.Application.Features.TestAttemps.Commands.StartAttempt
{
    public class StartAttemptCommandValidator : AbstractValidator<StartAttemptCommand>
    {
        public StartAttemptCommandValidator()
        {
            RuleFor(r => r.UserId)
                .NotEmpty().WithMessage("Empty user")
                .NotNull().WithMessage("Empty user");

            RuleFor(r => r.TestId)
                .NotNull().WithMessage("Test doesn't exist")
                .NotEmpty().WithMessage("Test doesn't exist");
        }
    }
}
