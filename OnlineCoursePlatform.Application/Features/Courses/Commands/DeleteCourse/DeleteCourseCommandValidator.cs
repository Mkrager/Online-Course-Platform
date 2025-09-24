using OnlineCoursePlatform.Application.Common.Validators;
using OnlineCoursePlatform.Application.Contracts.Persistance;

namespace OnlineCoursePlatform.Application.Features.Courses.Commands.DeleteCourse
{
    public class DeleteCourseCommandValidator : AccessValidator<DeleteCourseCommand>
    {
        public DeleteCourseCommandValidator(ICourseRepository courseRepository) : base(courseRepository)
        {
        }

        protected async override Task<bool> HasAccess(DeleteCourseCommand model, CancellationToken token)
        {
            return await _courseRepository.IsUserCourseTeacherAsync(model.UserId, model.Id);
        }
    }
}
