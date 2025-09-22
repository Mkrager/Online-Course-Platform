using FluentValidation;
using OnlineCoursePlatform.Application.Contracts.Application;

namespace OnlineCoursePlatform.Application.Common.Validators
{
    public abstract class CourseAccessValidator<T> : AbstractValidator<T>
    {
        protected readonly IPermissionService _permissionService;

        protected CourseAccessValidator(IPermissionService permissionService)
        {
            _permissionService = permissionService;

            RuleFor(x => x)
                .MustAsync(HasAccess)
                .WithMessage("You don't have access to this course");
        }

        protected abstract Task<bool> HasAccess(T model, CancellationToken token);
    }
}