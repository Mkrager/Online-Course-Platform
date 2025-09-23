using FluentValidation;

namespace OnlineCoursePlatform.Application.Features.TestAttemps.Commands.EndAttempt
{
    public class EndAttemptCommandValidator : AbstractValidator<EndAttemptCommand>
    {
        public EndAttemptCommandValidator()
        {
            RuleFor(r => r.AttempId)
                .NotEmpty().WithMessage("Empty attempt")
                .NotNull().WithMessage("Empty attempt");

            RuleFor(r => r.UserAnswerDto)
                .NotNull().WithMessage("User answers is null");

            RuleFor(r => r.UserAnswerDto.Count)
                .GreaterThan(0).WithMessage("Empty user answers")
                .When(r => r.UserAnswerDto != null);

            RuleForEach(r => r.UserAnswerDto)
                .SetValidator(new UserAnswerDtoValidator())
                .When(r => r.UserAnswerDto != null);
        }
    }
}