using OnlineCoursePlatform.Application.Common.Filters;
using OnlineCoursePlatform.Application.Common.Validators;
using OnlineCoursePlatform.Application.Contracts.Application;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Exceptions;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Features.CoursePublishRequests.Commands.UpdateCoursePublishRequestStatus.CancelCourse
{
    public class CancelCoursePublishRequestCommandValidator : AccessValidator<CancelCoursePublishRequestCommand, ICourseRepository>
    {
        public CancelCoursePublishRequestCommandValidator(ICourseRepository service, IPermissionService permissionService, string? errorMessage = null) 
            : base(service, permissionService, errorMessage)
        {
        }

        protected async override Task<bool> HasAccessInternal(CancelCoursePublishRequestCommand model, CancellationToken token)
        {
            var course = await _service.GetCourseAsync(new CourseFilter() { CoursePublishRequestId = model.Id });

            if (course == null)
                throw new NotFoundException(nameof(Course), model.Id);

            return await _service.IsUserCourseTeacherAsync(model.UserId, course.Id);
        }
    }
}
