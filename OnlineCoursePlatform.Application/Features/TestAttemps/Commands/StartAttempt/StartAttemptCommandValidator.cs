using FluentValidation;
using OnlineCoursePlatform.Application.Common.Validators;
using OnlineCoursePlatform.Application.Contracts.Application;

namespace OnlineCoursePlatform.Application.Features.TestAttemps.Commands.StartAttempt
{
    public class StartAttemptCommandValidator : CourseAccessValidator<StartAttemptCommand>
    {
        public StartAttemptCommandValidator(IPermissionService permissionService) : base(permissionService)
        {
            RuleFor(r => r.TestId)
                .NotNull().WithMessage("Test doesn't exist")
                .NotEmpty().WithMessage("Test doesn't exist");
        }

        protected override Task<bool> HasAccess(StartAttemptCommand model, CancellationToken token)
        {
            return _permissionService.HasUserCoursePermissionAsync(model.CourseId, model.UserId);
        }
    }
}
