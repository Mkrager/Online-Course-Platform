using OnlineCoursePlatform.Application.Common.Validators;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Features.CoursePublishRequests.Commands.CreateCoursePublishRequest;

namespace OnlineCoursePlatform.Application.Features.CoursePublishRequests.Commands.UpdateCoursePublishRequestStatus.CancelCourse
{
    internal class CancelCoursePublishRequestCommandValidator : AccessValidator<CancelCoursePublishRequestCommand>
    {
        public CancelCoursePublishRequestCommandValidator(ICourseRepository courseRepository) : base(courseRepository)
        {
        }

        protected override Task<bool> HasAccess(CancelCoursePublishRequestCommand model, CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }
}
