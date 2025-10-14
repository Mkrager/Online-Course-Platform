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

            RuleFor(e => e)
                .MustAsync(HasUserPendingRequest)
                .WithMessage("You already have a pending application.");
        }

        private async Task<bool> HasUserPendingRequest(CreateTeacherApplicationCommand model, CancellationToken token)
        {
            var pendingApplciations = await _teacherApplicationRepository.GetRequestsByUserIdAndStatusAsync(model.UserId, RequestStatus.Pending);

            if(pendingApplciations.Count == 0)
                return true;

            return false;
        }
    }
}