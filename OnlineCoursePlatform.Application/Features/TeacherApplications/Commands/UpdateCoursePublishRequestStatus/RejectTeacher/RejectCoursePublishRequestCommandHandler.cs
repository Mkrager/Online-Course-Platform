using MediatR;
using OnlineCoursePlatform.Application.Common.Handlers.Commands;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Features.TeacherApplications.Commands.UpdateCoursePublishRequestStatus.RejectTeacher
{
    public class RejectCoursePublishRequestCommandHandler
        : BaseUpdateRequestStatusCommandHandler<TeacherApplication, RejectCoursePublishRequestCommand>
    {
        public RejectCoursePublishRequestCommandHandler(IRequestRepository<TeacherApplication> repository) : base(repository)
        {
        }

        protected override Guid GetId(RejectCoursePublishRequestCommand request) => request.Id;

        protected override async Task HandleRequestAsync(TeacherApplication entity, RejectCoursePublishRequestCommand request, CancellationToken cancellationToken)
        {
            await UpdateStatusAsync(entity, Domain.Enums.RequestStatus.Rejected, request.RejectReason);
        }
    }
}