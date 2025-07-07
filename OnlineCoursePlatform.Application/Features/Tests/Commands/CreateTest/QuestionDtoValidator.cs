using FluentValidation;

namespace OnlineCoursePlatform.Application.Features.Tests.Commands.CreateTest
{
    public class QuestionDtoValidator : AbstractValidator<QuestionDto>
    {
        public QuestionDtoValidator()
        {
            RuleFor(q => q.Text)
                .NotEmpty()
                .WithMessage("Question text is required.");

            RuleFor(q => q.Answers)
                .NotNull().WithMessage("Answers list is required.");

            RuleFor(q => q.Answers.Count)
                .GreaterThan(0)
                .WithMessage("Each question must have at least one answer.")
                .When(q => q.Answers != null);

            RuleForEach(q => q.Answers)
                .SetValidator(new AnswerDtoValidator());

            RuleFor(q => q.Answers)
                .Must(answers => answers != null && answers.Any(a => a.IsCorrect))
                .WithMessage("Each question must have at least one correct answer.");
        }
    }
}
