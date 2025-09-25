using OnlineCoursePlatform.Application.Common.Validators;
using OnlineCoursePlatform.Application.Contracts.Persistance;

namespace OnlineCoursePlatform.Application.Features.Courses.Commands.DeleteCourse
{
    public class DeleteCourseCommandValidator : AccessValidator<DeleteCourseCommand, ICourseRepository>
    {
        public DeleteCourseCommandValidator(ICourseRepository service, string? errorMessage = null) 
            : base(service, errorMessage)
        {
        }

        protected async override Task<bool> HasAccessInternal(DeleteCourseCommand model, CancellationToken token)
        {
            return await _service.IsUserCourseTeacherAsync(model.UserId, model.Id);
        }
    }
}
