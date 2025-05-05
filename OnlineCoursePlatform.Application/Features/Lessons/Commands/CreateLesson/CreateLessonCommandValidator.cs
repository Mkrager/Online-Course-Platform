using FluentValidation;

namespace OnlineCoursePlatform.Application.Features.Lessons.Commands.CreateLesson
{
    public class CreateLessonCommandValidator : AbstractValidator<CreateLessonCommand>
    {
        public CreateLessonCommandValidator()
        {
            RuleFor(p => p.Title)
                .NotNull()
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");

            RuleFor(p => p.Order)
                .NotNull()
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .GreaterThan(0).WithMessage("{PropertyName} must be grather than 0");

            RuleFor(p => p.CourseId)
                .NotNull()
                .NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(p => p.Description)
                .NotNull()
                .NotEmpty().WithMessage("{PropertyName} is required.");
        }
    }
}
