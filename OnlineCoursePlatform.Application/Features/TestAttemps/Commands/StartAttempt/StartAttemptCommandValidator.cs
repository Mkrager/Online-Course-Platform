using FluentValidation;

namespace OnlineCoursePlatform.Application.Features.TestAttemps.Commands.StartAttempt
{
    public class StartAttemptCommandValidator : AbstractValidator<StartAttemptCommand>
    {
        public StartAttemptCommandValidator()
        {
            RuleFor(r => r.TestId)
                .NotNull().WithMessage("Test doesn't exist")
                .NotEmpty().WithMessage("Test doesn't exist");
        }
    }
}
