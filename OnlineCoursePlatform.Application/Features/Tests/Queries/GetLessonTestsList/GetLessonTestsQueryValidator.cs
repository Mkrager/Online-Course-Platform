using OnlineCoursePlatform.Application.Common.Filters;
using OnlineCoursePlatform.Application.Common.Validators;
using OnlineCoursePlatform.Application.Contracts.Application;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Exceptions;
using OnlineCoursePlatform.Application.Features.Tests.Queries.GetUserTestsList;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Features.Tests.Queries.GetLessonTestsList
{
    public class GetLessonTestsQueryValidator : CourseAccessValidator<GetLessonTestsQuery>
    {
        private readonly ICourseRepository _courseRepository;
        public GetLessonTestsQueryValidator(IPermissionService permissionService, ICourseRepository courseRepository) : base(permissionService)
        {
            _courseRepository = courseRepository;
        }

        protected override async Task<bool> HasAccess(GetLessonTestsQuery model, CancellationToken token)
        {
            var course = await _courseRepository.GetCourseAsync(new CourseFilter() { LessonId = model.LessonId});

            if (course == null)
                throw new NotFoundException(nameof(Course), model.LessonId);

            return await _permissionService.HasUserCoursePermissionAsync(course.Id, model.UserId);
        }
    }
}
