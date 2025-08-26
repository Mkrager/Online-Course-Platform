using FluentValidation;
using MediatR;

namespace OnlineCoursePlatform.Application.Features.CoursePublishRequests.Commands.CreateCoursePublishRequest
{
    public class CreateCoursePublishRequestValidator : AbstractValidator<CreateCoursePublishRequestCommand>
    {
        public CreateCoursePublishRequestValidator()
        {
            RuleFor(r => r.CourseId)
                .NotEmpty().WithMessage("CourseId required");
        }
    }
}
