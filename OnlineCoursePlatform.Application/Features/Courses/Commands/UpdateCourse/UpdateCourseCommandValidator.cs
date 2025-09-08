using FluentValidation;

namespace OnlineCoursePlatform.Application.Features.Courses.Commands.UpdateCourse
{
    public class UpdateCourseCommandValidator : AbstractValidator<UpdateCourseCommand>
    {
        public UpdateCourseCommandValidator()
        {
            RuleFor(p => p.Title)
                .NotNull()
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");

            RuleFor(p => p.Description)
                .NotNull()
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .MinimumLength(50).WithMessage("{PropertyName} must not less 50 characters.");
            RuleFor(p => p.Price)
                .NotNull()
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .GreaterThan(0);
            RuleFor(p => p.ThumbnailUrl)
                .NotEmpty()
                .NotNull().WithMessage("{PropertyName} is required.");
            RuleFor(p => p.CategoryId)
                .NotEmpty()
                .NotNull().WithMessage("{PropertyName} is required.");
        }
    }
}
