using MediatR;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Exceptions;
using OnlineCoursePlatform.Domain.Entities;
using OnlineCoursePlatform.Domain.Enums;

namespace OnlineCoursePlatform.Application.Features.CoursePublishRequests.Commands.UpdateCoursePublishRequestStatus.RejectCourse
{
    public class RejectCoursePublishRequestCommandHandler : IRequestHandler<RejectCoursePublishRequestCommand>
    {
        private readonly IRequestRepository<CoursePublishRequest> _requestRepository;
        public RejectCoursePublishRequestCommandHandler(IRequestRepository<CoursePublishRequest> requestRepository)
        {
            _requestRepository = requestRepository;
        }
        public async Task<Unit> Handle(RejectCoursePublishRequestCommand request, CancellationToken cancellationToken)
        {
            var coursePublishRequest = await _requestRepository.GetByIdAsync(request.Id);

            if (coursePublishRequest == null)
                throw new NotFoundException(nameof(CoursePublishRequest), request.Id);

            await _requestRepository.UpdateStatusAsync
                (coursePublishRequest, RequestStatus.Rejected, request.RejectReason);

            return Unit.Value;

        }
    }
}