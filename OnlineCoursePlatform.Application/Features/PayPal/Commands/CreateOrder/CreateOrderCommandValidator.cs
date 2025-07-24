using FluentValidation;
using OnlineCoursePlatform.Application.Contracts;
using OnlineCoursePlatform.Application.Contracts.Persistance;

namespace OnlineCoursePlatform.Application.Features.PayPal.Commands.CreateOrder
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        private readonly IEnrollmentRepository _enrollmentRepository;
        public CreateOrderCommandValidator(IEnrollmentRepository enrollmentRepository)
        {
            _enrollmentRepository = enrollmentRepository;

            RuleFor(r => r.CourseId)
                .NotEmpty()
                .WithMessage("CourseId is required");

            RuleFor(x => x)
                .MustAsync(async (model, cancellationToken) =>
                    !await IsUserEnrolledInCourse(model.UserId, model.CourseId, cancellationToken))
                .WithMessage("You already bought this course");
        }

        private async Task<bool> IsUserEnrolledInCourse(string userId, Guid courseId, CancellationToken token)
        {
            return await _enrollmentRepository.IsUserEnrolledInCourseAsync(userId, courseId);
        }
    }
}
