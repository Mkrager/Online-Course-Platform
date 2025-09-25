using FluentValidation;
using OnlineCoursePlatform.Application.Common.Interfaces;
using OnlineCoursePlatform.Application.Contracts.Application;

namespace OnlineCoursePlatform.Application.Common.Validators
{
    public abstract class AccessValidator<T, TService> : AbstractValidator<T> where T : IUserRequest
    {
        protected readonly TService _service;
        protected readonly IPermissionService _permissionService;
        protected AccessValidator(TService service, IPermissionService permissionService, string? errorMessage = null)
        {
            _service = service;
            _permissionService = permissionService;

            RuleFor(x => x)
                .MustAsync(HasAccess)
                .WithMessage(errorMessage ?? "You don't have access");
        }

        private async Task<bool> HasAccess(T model, CancellationToken token)
        {
            var permission = _permissionService.UserHasPrivilegedRole(model.UserRoles);

            if (permission)
                return true;

            return await HasAccessInternal(model, token);
        }

        protected abstract Task<bool> HasAccessInternal(T model, CancellationToken token);
    }
}
