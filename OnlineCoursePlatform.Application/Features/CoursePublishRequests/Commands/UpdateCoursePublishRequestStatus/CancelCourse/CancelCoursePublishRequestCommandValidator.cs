using OnlineCoursePlatform.Application.Common.Filters;
using OnlineCoursePlatform.Application.Common.Validators;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Exceptions;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Features.CoursePublishRequests.Commands.UpdateCoursePublishRequestStatus.CancelCourse
{
    internal class CancelCoursePublishRequestCommandValidator : AccessValidator<CancelCoursePublishRequestCommand>
    {
        public CancelCoursePublishRequestCommandValidator(ICourseRepository courseRepository) : base(courseRepository)
        {
        }

        protected async override Task<bool> HasAccess(CancelCoursePublishRequestCommand model, CancellationToken token)
        {
            var course = await _courseRepository.GetCourseAsync(new CourseFilter() { CoursePublishRequestId = model.Id });

            if (course == null)
                throw new NotFoundException(nameof(Course), model.Id);

            return await _courseRepository.IsUserCourseTeacherAsync(model.UserId, course.Id);
        }
    }
}
