using OnlineCoursePlatform.Application.Common.Handlers.Commands;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Domain.Entities;
using OnlineCoursePlatform.Domain.Enums;

namespace OnlineCoursePlatform.Application.Features.TeacherApplications.Commands.UpdateTeacherApplicationStatus.CancelTeacher
{
    public class CancelTeacherApplicationCommandHandler
        : BaseUpdateRequestStatusCommandHandler<TeacherApplication, CancelTeacherApplicationCommand>
    {
        public CancelTeacherApplicationCommandHandler(IRequestRepository<TeacherApplication> requestRepository)
            : base(requestRepository) { }

        protected override Guid GetId(CancelTeacherApplicationCommand request) => request.Id;

        protected override async Task HandleRequestAsync(TeacherApplication entity, CancelTeacherApplicationCommand request, CancellationToken cancellationToken)
        {
            await UpdateStatusAsync(entity, RequestStatus.Canceled);
        }
    }
}
