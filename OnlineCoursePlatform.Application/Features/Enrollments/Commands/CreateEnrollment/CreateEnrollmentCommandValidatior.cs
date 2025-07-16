using FluentValidation;

namespace OnlineCoursePlatform.Application.Features.Enrollments.Commands.CreateEnrollment
{
    public class CreateEnrollmentCommandValidatior : AbstractValidator<CreateEnrollmentCommand>
    {
        public CreateEnrollmentCommandValidatior()
        {
            RuleFor(r => r.CourseId)
                .NotEmpty()
                .WithMessage("CourseId is required");
        }
    }
}
