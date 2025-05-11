using FluentValidation;

namespace OnlineCoursePlatform.Application.Features.Tests.Commands.CreateTest
{
    public class AnswerDtoValidator : AbstractValidator<AnswerDto>
    {
        public AnswerDtoValidator()
        {
            RuleFor(a => a.Text)
                .NotEmpty()
                .WithMessage("Answer text is required.");
        }
    }
}
