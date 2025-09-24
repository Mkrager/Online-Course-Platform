using FluentValidation;
using OnlineCoursePlatform.Application.Common.Validators;
using OnlineCoursePlatform.Application.Contracts.Persistance;

namespace OnlineCoursePlatform.Application.Features.CoursePublishRequests.Commands.CreateCoursePublishRequest
{
    public class CreateCoursePublishRequestCommandValidator : AccessValidator<CreateCoursePublishRequestCommand>
    {
        public CreateCoursePublishRequestCommandValidator(ICourseRepository courseRepository) : base(courseRepository)
        {
            RuleFor(r => r.CourseId)
                .NotEmpty().WithMessage("CourseId required");
        }

        protected override async Task<bool> HasAccess(CreateCoursePublishRequestCommand model, CancellationToken token)
        {
            return await _courseRepository.IsUserCourseTeacherAsync(model.UserId, model.CourseId);
        }
    }
}
