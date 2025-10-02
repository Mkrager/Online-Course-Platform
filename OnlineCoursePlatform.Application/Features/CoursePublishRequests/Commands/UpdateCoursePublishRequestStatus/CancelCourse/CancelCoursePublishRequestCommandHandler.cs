using MediatR;
using OnlineCoursePlatform.Application.Common.Handlers.Commands;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Exceptions;
using OnlineCoursePlatform.Domain.Entities;
using OnlineCoursePlatform.Domain.Enums;

namespace OnlineCoursePlatform.Application.Features.CoursePublishRequests.Commands.UpdateCoursePublishRequestStatus.CancelCourse
{
    public class CancelCoursePublishRequestCommandHandler
        : BaseUpdateRequestStatusCommandHandler<CoursePublishRequest, CancelCoursePublishRequestCommand>
    {
        public CancelCoursePublishRequestCommandHandler(IRequestRepository<CoursePublishRequest> requestRepository)
            : base(requestRepository) { }

        protected override Guid GetId(CancelCoursePublishRequestCommand request) => request.Id;

        protected override async Task HandleRequestAsync(CoursePublishRequest entity, CancelCoursePublishRequestCommand request, CancellationToken cancellationToken)
        {
            await UpdateStatusAsync(entity, RequestStatus.Canceled);
        }
    }
}
