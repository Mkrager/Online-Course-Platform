using FluentValidation;

namespace OnlineCoursePlatform.Application.Features.TestAttemps.Commands.EndAttempt
{
    public class UserAnswerDtoValidator : AbstractValidator<UserAnswerDto>
    {
        public UserAnswerDtoValidator()
        {
            RuleFor(r => r.AnswerId)
                .NotNull().WithMessage("Empty answer")
                .NotEmpty().WithMessage("Empty answer");

            RuleFor(r => r.QuestionId)
                .NotNull().WithMessage("Empty question")
                .NotEmpty().WithMessage("Empty question");
        }
    }
}
