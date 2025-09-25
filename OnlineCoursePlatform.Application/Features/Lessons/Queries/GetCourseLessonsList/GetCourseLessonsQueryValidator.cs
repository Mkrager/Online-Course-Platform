using OnlineCoursePlatform.Application.Common.Validators;
using OnlineCoursePlatform.Application.Contracts.Application;

namespace OnlineCoursePlatform.Application.Features.Lessons.Queries.GetCourseLessonsList
{
    public class GetCourseLessonsQueryValidator : AccessValidator<GetCourseLessonsQuery, IPermissionService>
    {
        public GetCourseLessonsQueryValidator(IPermissionService service, string? errorMessage = null) 
            : base(service, errorMessage)
        {
        }

        protected override async Task<bool> HasAccessInternal(GetCourseLessonsQuery model, CancellationToken token)
        {
            return await _service.HasUserCoursePermissionAsync(model.CourseId, model.UserId);
        }
    }
}