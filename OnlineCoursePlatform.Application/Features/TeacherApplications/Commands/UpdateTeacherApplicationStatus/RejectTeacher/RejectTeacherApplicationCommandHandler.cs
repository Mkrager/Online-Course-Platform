using OnlineCoursePlatform.Application.Common.Handlers.Commands;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Features.TeacherApplications.Commands.UpdateTeacherApplicationStatus.RejectTeacher
{
    public class RejectTeacherApplicationCommandHandler
        : BaseUpdateRequestStatusCommandHandler<TeacherApplication, RejectTeacherApplicationCommand>
    {
        public RejectTeacherApplicationCommandHandler(IRequestRepository<TeacherApplication> repository) : base(repository)
        {
        }

        protected override Guid GetId(RejectTeacherApplicationCommand request) => request.Id;

        protected override async Task HandleRequestAsync(TeacherApplication entity, RejectTeacherApplicationCommand request, CancellationToken cancellationToken)
        {
            await UpdateStatusAsync(entity, Domain.Enums.RequestStatus.Rejected, request.RejectReason);
        }
    }
}