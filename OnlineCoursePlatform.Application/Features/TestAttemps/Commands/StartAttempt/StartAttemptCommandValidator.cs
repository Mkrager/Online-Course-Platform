using FluentValidation;
using OnlineCoursePlatform.Application.Common.Filters;
using OnlineCoursePlatform.Application.Common.Validators;
using OnlineCoursePlatform.Application.Contracts.Application;
using OnlineCoursePlatform.Application.Contracts.Persistance;

namespace OnlineCoursePlatform.Application.Features.TestAttemps.Commands.StartAttempt
{
    public class StartAttemptCommandValidator : AccessValidator<StartAttemptCommand, ICourseRepository>
    {
        public StartAttemptCommandValidator(ICourseRepository service, IPermissionService permissionService, string? errorMessage = null)
            : base(service, permissionService, errorMessage)
        {
            RuleFor(r => r.TestId)
                .NotNull().WithMessage("Test doesn't exist")
                .NotEmpty().WithMessage("Test doesn't exist");
        }

        protected override async Task<bool> HasAccessInternal(StartAttemptCommand model, CancellationToken token)
        {
            var course = await _service.GetCourseAsync(new CourseFilter() { TestId = model.TestId });

            if (course == null)
                return false;

            return await _permissionService.HasUserCoursePermissionAsync(course, model.UserId);
        }
    }
}