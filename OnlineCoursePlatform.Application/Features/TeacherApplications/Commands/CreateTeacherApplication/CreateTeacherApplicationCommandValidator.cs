using FluentValidation;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Domain.Entities;
using OnlineCoursePlatform.Domain.Enums;

namespace OnlineCoursePlatform.Application.Features.TeacherApplications.Commands.CreateTeacherApplication
{
    public class CreateTeacherApplicationCommandValidator : AbstractValidator<CreateTeacherApplicationCommand>
    {
        private readonly IRequestRepository<TeacherApplication> _teacherApplicationRepository;

        public CreateTeacherApplicationCommandValidator(IRequestRepository<TeacherApplication> teacherApplicationRepository)
        {
            _teacherApplicationRepository = teacherApplicationRepository;

            RuleFor(e => e.UserId)
                .MustAsync(HasUserPendingRequest)
                .WithMessage("You already have a pending application.");
        }

        private async Task<bool> HasUserPendingRequest(string? userId, CancellationToken token)
        {
            var pendingApplciations = await _teacherApplicationRepository.GetRequestsByUserIdAndStatusAsync(userId, RequestStatus.Pending);

            if(pendingApplciations.Count == 0)
                return true;

            return false;
        }
    }
}