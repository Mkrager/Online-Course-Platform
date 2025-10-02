using MediatR;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Exceptions;
using OnlineCoursePlatform.Domain.Entities;
using OnlineCoursePlatform.Domain.Enums;

namespace OnlineCoursePlatform.Application.Features.CoursePublishRequests.Commands.UpdateCoursePublishRequestStatus.ApproveCourse
{
    public class ApproveCoursePublishRequestCommandHandler : IRequestHandler<ApproveCoursePublishRequestCommand>
    {
        private readonly IRequestRepository<CoursePublishRequest> _requestRepository;
        public ApproveCoursePublishRequestCommandHandler(IRequestRepository<CoursePublishRequest> requestRepository)
        {
            _requestRepository = requestRepository;
        }
        public async Task<Unit> Handle(ApproveCoursePublishRequestCommand request, CancellationToken cancellationToken)
        {
            var coursePublishRequest = await _requestRepository.GetByIdAsync(request.Id);

            if (coursePublishRequest == null)
                throw new NotFoundException(nameof(CoursePublishRequest), request.Id);

            await _requestRepository.UpdateStatusAsync(coursePublishRequest, RequestStatus.Approved);

            return Unit.Value;
        }
    }
}
