using MediatR;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Exceptions;
using OnlineCoursePlatform.Application.Features.Courses.Commands.PublishCourse;
using OnlineCoursePlatform.Domain.Entities;
using OnlineCoursePlatform.Domain.Enums;

namespace OnlineCoursePlatform.Application.Features.CoursePublishRequests.Commands.UpdateCoursePublishRequestStatus.ApproveCourse
{
    public class ApproveCoursePublishRequestCommandHandler : IRequestHandler<ApproveCoursePublishRequestCommand>
    {
        private readonly IRequestRepository<CoursePublishRequest> _requestRepository;
        private readonly IMediator _mediator;
        public ApproveCoursePublishRequestCommandHandler(IRequestRepository<CoursePublishRequest> requestRepository, IMediator mediator)
        {
            _requestRepository = requestRepository;
            _mediator = mediator;
        }
        public async Task<Unit> Handle(ApproveCoursePublishRequestCommand request, CancellationToken cancellationToken)
        {
            var coursePublishRequest = await _requestRepository.GetByIdAsync(request.Id);

            if (coursePublishRequest == null)
                throw new NotFoundException(nameof(CoursePublishRequest), request.Id);

            await _requestRepository.UpdateStatusAsync(coursePublishRequest, RequestStatus.Approved);
            await _mediator.Send(new PublishCourseCommand() { Id = coursePublishRequest.CourseId });
            return Unit.Value;
        }
    }
}
