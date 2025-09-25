using OnlineCoursePlatform.Application.Common.Filters;
using OnlineCoursePlatform.Application.Common.Validators;
using OnlineCoursePlatform.Application.Contracts.Application;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Exceptions;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Features.Tests.Queries.GetTestDetail
{
    public class GetTestDetailQueryValidator : AccessValidator<GetTestDetailQuery, ICourseRepository>
    {
        public GetTestDetailQueryValidator(ICourseRepository service, IPermissionService permissionService, string? errorMessage = null) : base(service, permissionService, errorMessage)
        {
        }

        protected async override Task<bool> HasAccessInternal(GetTestDetailQuery model, CancellationToken token)
        {
            var course = await _service.GetCourseAsync(new CourseFilter() { TestId = model.Id });

            if (course == null)
                throw new NotFoundException(nameof(Course), model.Id);

            return await _permissionService.HasUserCoursePermissionAsync(course.Id, model.UserId);
        }
    }
}