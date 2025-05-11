using FluentValidation;

namespace OnlineCoursePlatform.Application.Features.Tests.Commands.CreateTest
{
    public class CreateTestValidator : AbstractValidator<CreateTestCommand>
    {
        public CreateTestValidator()
        {
            RuleFor(p => p.Title)
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(p => p.Questions.Count)
                .GreaterThan(0)
                .WithMessage("There must be at least one question.");

            RuleForEach(p => p.Questions)
                .SetValidator(new QuestionDtoValidator());
        }
    }
}
