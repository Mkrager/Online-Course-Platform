using FluentValidation;
using OnlineCoursePlatform.Application.Contracts.Application;
using OnlineCoursePlatform.Application.Contracts.Persistance;

namespace OnlineCoursePlatform.Application.Features.Lessons.Queries.GetCourseLessonsList
{
    public class GetCourseLessonsQueryValidator : AbstractValidator<GetCourseLessonsQuery>
    {
        private readonly IPermissionService _permissionService;
        public GetCourseLessonsQueryValidator(IPermissionService permissionService)
        {
            _permissionService = permissionService;

            RuleFor(x => x)
                .MustAsync(async (model, cancellationToken) =>
                    await HasAccessToCourse(model.UserId, model.CourseId, cancellationToken))
                .WithMessage("You don't have access to this course");
        }

        private async Task<bool> HasAccessToCourse(string userId, Guid courseId, CancellationToken token)
        {
            return await _permissionService.HasUserCoursePermissionAsync(courseId, userId);
        }
    }
}