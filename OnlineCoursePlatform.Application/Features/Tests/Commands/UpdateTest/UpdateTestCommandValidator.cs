using FluentValidation;
using OnlineCoursePlatform.Application.Common.Filters;
using OnlineCoursePlatform.Application.Common.Validators;
using OnlineCoursePlatform.Application.Contracts.Application;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Exceptions;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Features.Tests.Commands.UpdateTest
{
    public class UpdateTestCommandValidator : AccessValidator<UpdateTestCommand, ICourseRepository>
    {
        public UpdateTestCommandValidator(ICourseRepository service, IPermissionService permissionService, string? errorMessage = null) 
            : base(service, permissionService, errorMessage)
        {
        }

        protected override async Task<bool> HasAccessInternal(UpdateTestCommand model, CancellationToken token)
        {
            var course = await _service.GetCourseAsync(new CourseFilter() { TestId = model.Id });

            if (course == null)
                throw new NotFoundException(nameof(Course), model.Id);

            return await _service.IsUserCourseTeacherAsync(model.UserId, course.Id);
        }
    }
}