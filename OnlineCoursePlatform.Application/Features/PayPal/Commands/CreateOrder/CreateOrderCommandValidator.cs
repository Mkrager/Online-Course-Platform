using FluentValidation;
using OnlineCoursePlatform.Application.Contracts;
using OnlineCoursePlatform.Application.Contracts.Persistance;

namespace OnlineCoursePlatform.Application.Features.PayPal.Commands.CreateOrder
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly ICurrentUserService _currentUserService;
        public CreateOrderCommandValidator(IEnrollmentRepository enrollmentRepository, ICurrentUserService currentUserService)
        {
            _enrollmentRepository = enrollmentRepository;
            _currentUserService = currentUserService;

            RuleFor(r => r.CourseId)
                .NotEmpty()
                .WithMessage("CourseId is required");

            RuleFor(x => x)
                .MustAsync(async (model, cancellationToken) =>
                    !await IsUserEnrolledInCourse(_currentUserService.UserId, model.CourseId, cancellationToken))
                .WithMessage("You already bought this course");
        }

        private async Task<bool> IsUserEnrolledInCourse(string userId, Guid courseId, CancellationToken token)
        {
            return await _enrollmentRepository.IsUserEnrolledInCourseAsync(userId, courseId);
        }
    }
}
