using FluentValidation;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Common.Validators;

namespace OnlineCoursePlatform.Application.Features.CoursePublishRequests.Commands.CreateCoursePublishRequest
{
    public class CreateCoursePublishRequestCommandValidator : AccessValidator<CreateCoursePublishRequestCommand, ICourseRepository>
    {
        public CreateCoursePublishRequestCommandValidator(ICourseRepository service, string? errorMessage = null) 
            : base(service, errorMessage)
        {
            RuleFor(r => r.CourseId)
                .NotEmpty().WithMessage("CourseId required");
        }

        protected override async Task<bool> HasAccessInternal(CreateCoursePublishRequestCommand model, CancellationToken token)
        {
            return await _service.IsUserCourseTeacherAsync(model.UserId, model.CourseId);
        }
    }
}
