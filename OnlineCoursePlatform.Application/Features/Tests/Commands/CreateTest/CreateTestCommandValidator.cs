using FluentValidation;

namespace OnlineCoursePlatform.Application.Features.Tests.Commands.CreateTest
{
    public class CreateTestCommandValidator : AbstractValidator<CreateTestCommand>
    {
        public CreateTestCommandValidator()
        {
            RuleFor(p => p.Title)
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(p => p.LessonId)
                .NotEmpty()
                .WithMessage("Empty lesson");

            RuleFor(p => p.Questions)
                .NotNull().WithMessage("Questions list is required.");

            RuleFor(p => p.Questions.Count)
                .GreaterThan(0)
                .WithMessage("There must be at least one question.")
                .When(p => p.Questions != null);

            RuleForEach(p => p.Questions)
                .SetValidator(new QuestionDtoValidator());
        }
    }
}
