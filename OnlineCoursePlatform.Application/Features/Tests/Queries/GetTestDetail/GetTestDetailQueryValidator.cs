using OnlineCoursePlatform.Application.Common.Filters;
using OnlineCoursePlatform.Application.Common.Validators;
using OnlineCoursePlatform.Application.Contracts.Application;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Exceptions;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Features.Tests.Queries.GetTestDetail
{
    public class GetTestDetailQueryValidator : CourseAccessValidator<GetTestDetailQuery>
    {
        private readonly ICourseRepository _courseRepository;
        public GetTestDetailQueryValidator(IPermissionService permissionService, ICourseRepository courseRepository) : base(permissionService)
        {
            _courseRepository = courseRepository;
        }

        protected async override Task<bool> HasAccess(GetTestDetailQuery model, CancellationToken token)
        {
            var course = await _courseRepository.GetCourseAsync(new CourseFilter() { TestId = model.Id });

            if (course == null)
                throw new NotFoundException(nameof(Course), model.Id);

            return await _permissionService.HasUserCoursePermissionAsync(course.Id, model.UserId);
        }
    }
}
