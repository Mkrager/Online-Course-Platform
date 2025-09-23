using OnlineCoursePlatform.Application.Common.Validators;
using OnlineCoursePlatform.Application.Contracts.Application;

namespace OnlineCoursePlatform.Application.Features.Lessons.Queries.GetCourseLessonsList
{
    public class GetCourseLessonsQueryValidator : CourseAccessValidator<GetCourseLessonsQuery>
    {
        public GetCourseLessonsQueryValidator(IPermissionService permissionService) : base(permissionService)
        {
        }

        protected override async Task<bool> HasAccess(GetCourseLessonsQuery model, CancellationToken token)
        {
            return await _permissionService.HasUserCoursePermissionAsync(model.CourseId, model.UserId);
        }
    }
}