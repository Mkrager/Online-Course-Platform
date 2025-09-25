using OnlineCoursePlatform.Application.Common.Validators;
using OnlineCoursePlatform.Application.Contracts.Application;
using OnlineCoursePlatform.Application.Contracts.Persistance;

namespace OnlineCoursePlatform.Application.Features.Courses.Commands.UnPublishCourse
{
    public class UnPublishCourseCommandValidator : AccessValidator<UnPublishCourseCommand, ICourseRepository>
    {
        public UnPublishCourseCommandValidator(ICourseRepository service, IPermissionService permissionService, string? errorMessage = null) 
            : base(service, permissionService, errorMessage)
        {
        }

        protected async override Task<bool> HasAccessInternal (UnPublishCourseCommand model, CancellationToken token)
        {
            return await _service.IsUserCourseTeacherAsync(model.UserId, model.Id);   
        }
    }
}
