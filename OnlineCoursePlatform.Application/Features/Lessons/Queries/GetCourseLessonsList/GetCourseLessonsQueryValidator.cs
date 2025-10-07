using OnlineCoursePlatform.Application.Common.Validators;
using OnlineCoursePlatform.Application.Contracts.Application;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Exceptions;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Features.Lessons.Queries.GetCourseLessonsList
{
    public class GetCourseLessonsQueryValidator : AccessValidator<GetCourseLessonsQuery, ICourseRepository>
    {
        public GetCourseLessonsQueryValidator(ICourseRepository service, IPermissionService permissionService, string? errorMessage = null) : base(service, permissionService, errorMessage)
        {
        }

        protected override async Task<bool> HasAccessInternal(GetCourseLessonsQuery model, CancellationToken token)
        {
            var course = await _service.GetByIdAsync(model.CourseId);

            if (course == null)
                throw new NotFoundException(nameof(Course), model.CourseId);

            return await _permissionService.HasUserCoursePermissionAsync(course, model.UserId);
        }
    }
}