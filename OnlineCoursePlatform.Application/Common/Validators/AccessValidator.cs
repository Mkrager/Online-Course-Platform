using FluentValidation;
using OnlineCoursePlatform.Application.Contracts.Persistance;

namespace OnlineCoursePlatform.Application.Common.Validators
{
    public abstract class AccessValidator<T> : AbstractValidator<T>
    {
        protected readonly ICourseRepository _courseRepository;
        protected AccessValidator(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;

            RuleFor(x => x)
                .MustAsync(HasAccess)
                .WithMessage("You don't have access");
        }

        protected abstract Task<bool> HasAccess(T model, CancellationToken token);
    }
}
