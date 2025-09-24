using FluentValidation;
using OnlineCoursePlatform.Application.Common.Validators;
using OnlineCoursePlatform.Application.Contracts.Persistance;

namespace OnlineCoursePlatform.Application.Features.Courses.Commands.UpdateCourse
{
    public class UpdateCourseCommandValidator : AccessValidator<UpdateCourseCommand>
    {
        public UpdateCourseCommandValidator(ICourseRepository courseRepository) : base(courseRepository)
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

        protected override async Task<bool> HasAccess(UpdateCourseCommand model, CancellationToken token)
        {
            return await _courseRepository.IsUserCourseTeacherAsync(model.UserId, model.Id);
        }
    }
}
