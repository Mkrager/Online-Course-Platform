using FluentValidation;
using OnlineCoursePlatform.Application.Common.Validators;
using OnlineCoursePlatform.Application.Contracts.Application;
using OnlineCoursePlatform.Application.Contracts.Persistance;

namespace OnlineCoursePlatform.Application.Features.Lessons.Commands.CreateLesson
{
    public class CreateLessonCommandValidator : AccessValidator<CreateLessonCommand, ICourseRepository>
    {
        public CreateLessonCommandValidator(ICourseRepository service, IPermissionService permissionService, string? errorMessage = null) : base(service, permissionService, errorMessage)
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

        protected override async Task<bool> HasAccessInternal(CreateLessonCommand model, CancellationToken token)
        {
            return await _service.IsUserCourseTeacherAsync(model.UserId, model.CourseId);
        }
    }
}
