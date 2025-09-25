using FluentValidation;
using OnlineCoursePlatform.Application.Common.Interfaces;

namespace OnlineCoursePlatform.Application.Common.Validators
{
    public abstract class AccessValidator<T, TService> : AbstractValidator<T> where T : IUserRequest
    {
        protected readonly TService _service;

        protected AccessValidator(TService service, string? errorMessage = null)
        {
            _service = service;

            RuleFor(x => x)
                .MustAsync(HasAccess)
                .WithMessage(errorMessage ?? "You don't have access");
        }

        private async Task<bool> HasAccess(T model, CancellationToken token)
        {

            

            return await HasAccessInternal(model, token);
        }

        protected abstract Task<bool> HasAccessInternal(T model, CancellationToken token);
    }
}
