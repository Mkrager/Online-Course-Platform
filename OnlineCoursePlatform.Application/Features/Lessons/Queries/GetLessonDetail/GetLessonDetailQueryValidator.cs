using OnlineCoursePlatform.Application.Common.Validators;
using OnlineCoursePlatform.Application.Contracts.Application;
using OnlineCoursePlatform.Application.Features.Lessons.Queries.GetCourseLessonsList;

namespace OnlineCoursePlatform.Application.Features.Lessons.Queries.GetLessonDetail
{
    public class GetLessonDetailQueryValidator : CourseAccessValidator<GetCourseLessonsQuery>
    {
        public GetLessonDetailQueryValidator(IPermissionService permissionService) : base(permissionService)
        {
        }

        protected override Task<bool> HasAccess(GetCourseLessonsQuery model, CancellationToken token)
        {
            return _permissionService.HasUserCoursePermissionAsync(model.CourseId, model.UserId);
        }
    }
}