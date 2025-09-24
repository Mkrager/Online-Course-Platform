using OnlineCoursePlatform.Application.Common.Validators;
using OnlineCoursePlatform.Application.Contracts.Persistance;

namespace OnlineCoursePlatform.Application.Features.Courses.Commands.UnPublishCourse
{
    public class UnPublishCourseCommandValidator : AccessValidator<UnPublishCourseCommand>
    {
        public UnPublishCourseCommandValidator(ICourseRepository courseRepository) : base(courseRepository)
        {
        }

        protected async override Task<bool> HasAccess(UnPublishCourseCommand model, CancellationToken token)
        {
            return await _courseRepository.IsUserCourseTeacherAsync(model.UserId, model.Id);   
        }
    }
}
