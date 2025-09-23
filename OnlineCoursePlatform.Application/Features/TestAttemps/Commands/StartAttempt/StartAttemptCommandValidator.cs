using FluentValidation;
using OnlineCoursePlatform.Application.Common.Filters;
using OnlineCoursePlatform.Application.Common.Validators;
using OnlineCoursePlatform.Application.Contracts.Application;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Features.TestAttemps.Commands.StartAttempt
{
    public class StartAttemptCommandValidator : CourseAccessValidator<StartAttemptCommand>
    {
        private readonly ICourseRepository _courseRepository;

        public StartAttemptCommandValidator(IPermissionService permissionService, ICourseRepository courseRepository) : base(permissionService)
        {
            _courseRepository = courseRepository;

            RuleFor(r => r.TestId)
                .NotNull().WithMessage("Test doesn't exist")
                .NotEmpty().WithMessage("Test doesn't exist");
        }

        protected override async Task<bool> HasAccess(StartAttemptCommand model, CancellationToken token)
        {
            var course = await _courseRepository.GetCourseAsync(new CourseFilter() { TestId = model.TestId });

            if (course == null)
                return false;

            return await _permissionService.HasUserCoursePermissionAsync(course.Id, model.UserId);
        }
    }
}
