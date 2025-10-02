using MediatR;
using OnlineCoursePlatform.Application.Common.Handlers.Commands;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Features.Courses.Commands.PublishCourse;
using OnlineCoursePlatform.Domain.Entities;
using OnlineCoursePlatform.Domain.Enums;

namespace OnlineCoursePlatform.Application.Features.CoursePublishRequests.Commands.UpdateCoursePublishRequestStatus.ApproveCourse
{
    public class ApproveCoursePublishRequestCommandHandler
        : BaseUpdateRequestStatusCommandHandler<CoursePublishRequest, ApproveCoursePublishRequestCommand>
    {
        private readonly IMediator _mediator;

        public ApproveCoursePublishRequestCommandHandler(
            IRequestRepository<CoursePublishRequest> requestRepository,
            IMediator mediator)
            : base(requestRepository)
        {
            _mediator = mediator;
        }

        protected override Guid GetId(ApproveCoursePublishRequestCommand request) => request.Id;

        protected override async Task HandleRequestAsync(CoursePublishRequest entity, ApproveCoursePublishRequestCommand request, CancellationToken cancellationToken)
        {
            await UpdateStatusAsync(entity, RequestStatus.Approved);
            await _mediator.Send(new PublishCourseCommand { Id = entity.CourseId }, cancellationToken);
        }
    }
}