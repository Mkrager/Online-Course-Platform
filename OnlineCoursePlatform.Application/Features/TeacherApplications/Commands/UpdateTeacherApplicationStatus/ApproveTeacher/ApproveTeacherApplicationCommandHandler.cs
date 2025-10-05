using MediatR;
using OnlineCoursePlatform.Application.Common.Handlers.Commands;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Features.User.Commands.AssignRole.AssignTeacherRole;
using OnlineCoursePlatform.Domain.Entities;
using OnlineCoursePlatform.Domain.Enums;

namespace OnlineCoursePlatform.Application.Features.TeacherApplications.Commands.UpdateTeacherApplicationStatus.ApproveTeacher
{
    public class ApproveTeacherApplicationCommandHandler
                : BaseUpdateRequestStatusCommandHandler<TeacherApplication, ApproveTeacherApplicationCommand>
    {
        private readonly IMediator _mediator;
        public ApproveTeacherApplicationCommandHandler(IRequestRepository<TeacherApplication> repository, IMediator mediator) 
            : base(repository)
        {
            _mediator = mediator;
        }

        protected override Guid GetId(ApproveTeacherApplicationCommand request) => request.Id;

        protected override async Task HandleRequestAsync(TeacherApplication entity, ApproveTeacherApplicationCommand request, CancellationToken cancellationToken)
        {
            await UpdateStatusAsync(entity, RequestStatus.Approved);
            await _mediator.Send(new AssignTeacherRoleCommand { UserId = entity.CreatedBy }, cancellationToken);
        }
    }
}