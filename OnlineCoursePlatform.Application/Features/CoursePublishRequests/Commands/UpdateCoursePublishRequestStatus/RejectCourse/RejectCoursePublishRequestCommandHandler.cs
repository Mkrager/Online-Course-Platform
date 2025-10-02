using OnlineCoursePlatform.Application.Common.Handlers.Commands;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Features.CoursePublishRequests.Commands.UpdateCoursePublishRequestStatus.RejectCourse
{
    public class RejectCoursePublishRequestCommandHandler
        : BaseUpdateRequestStatusCommandHandler<CoursePublishRequest, RejectCoursePublishRequestCommand>
    {
        public RejectCoursePublishRequestCommandHandler(IRequestRepository<CoursePublishRequest> requestRepository)
            : base(requestRepository) { }

        protected override Guid GetId(RejectCoursePublishRequestCommand request) => request.Id;

        protected override async Task HandleRequestAsync(CoursePublishRequest entity, RejectCoursePublishRequestCommand request, CancellationToken cancellationToken)
        {
            await UpdateStatusAsync(entity, Domain.Enums.RequestStatus.Rejected, request.RejectReason);
        }
    }

}