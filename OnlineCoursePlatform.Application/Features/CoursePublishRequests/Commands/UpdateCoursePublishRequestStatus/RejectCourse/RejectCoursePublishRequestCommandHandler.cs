using MediatR;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Exceptions;
using OnlineCoursePlatform.Domain.Entities;
using OnlineCoursePlatform.Domain.Enums;

namespace OnlineCoursePlatform.Application.Features.CoursePublishRequests.Commands.UpdateCoursePublishRequestStatus.RejectCourse
{
    public class RejectCoursePublishRequestCommandHandler : IRequestHandler<RejectCoursePublishRequestCommand>
    {
        private readonly ICoursePublishRequestRepository _coursePublishRequestRepository;
        public RejectCoursePublishRequestCommandHandler(ICoursePublishRequestRepository coursePublishRequestRepository)
        {
            _coursePublishRequestRepository = coursePublishRequestRepository;
        }
        public async Task<Unit> Handle(RejectCoursePublishRequestCommand request, CancellationToken cancellationToken)
        {
            var coursePublishRequest = await _coursePublishRequestRepository.GetByIdAsync(request.Id);

            if (coursePublishRequest == null)
                throw new NotFoundException(nameof(CoursePublishRequest), request.Id);

            await _coursePublishRequestRepository.UpdateStatusAsync
                (coursePublishRequest, CoursePublishStatus.Rejected, request.RejectReason);

            return Unit.Value;

        }
    }
}