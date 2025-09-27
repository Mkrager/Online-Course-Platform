using FluentValidation;

namespace OnlineCoursePlatform.Application.Features.CoursePublishRequests.Commands.UpdateCoursePublishRequestStatus.RejectCourse
{
    public class RejectCoursePublishRequestValidator : AbstractValidator<RejectCoursePublishRequestCommand>
    {
        public RejectCoursePublishRequestValidator()
        {
            RuleFor(r => r.Id)
                .NotEmpty().WithMessage("Id must not be empty");

            RuleFor(r => r.RejectReason)
                .NotEmpty().WithMessage("Reject reason must not be empty");
        }
    }
}