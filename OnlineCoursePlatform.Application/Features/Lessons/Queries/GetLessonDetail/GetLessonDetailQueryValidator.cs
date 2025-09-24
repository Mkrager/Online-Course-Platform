using OnlineCoursePlatform.Application.Common.Filters;
using OnlineCoursePlatform.Application.Common.Validators;
using OnlineCoursePlatform.Application.Contracts.Application;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Exceptions;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Features.Lessons.Queries.GetLessonDetail
{
    public class GetLessonDetailQueryValidator : CourseAccessValidator<GetLessonDetailQuery>
    {
        private readonly ICourseRepository _courseRepository;
        public GetLessonDetailQueryValidator(IPermissionService permissionService, ICourseRepository courseRepository) : base(permissionService)
        {
            _courseRepository = courseRepository;
        }

        protected override async Task<bool> HasAccess(GetLessonDetailQuery model, CancellationToken token)
        {
            var course = await _courseRepository.GetCourseAsync(new CourseFilter() { LessonId = model.Id });

            if (course == null)
                throw new NotFoundException(nameof(Course), model.Id);

                return await _permissionService.HasUserCoursePermissionAsync(course.Id, model.UserId);
        }
    }
}