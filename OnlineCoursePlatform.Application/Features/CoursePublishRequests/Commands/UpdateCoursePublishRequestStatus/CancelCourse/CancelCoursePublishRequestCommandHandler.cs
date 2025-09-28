using MediatR;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Exceptions;
using OnlineCoursePlatform.Domain.Entities;
using OnlineCoursePlatform.Domain.Enums;

namespace OnlineCoursePlatform.Application.Features.CoursePublishRequests.Commands.UpdateCoursePublishRequestStatus.CancelCourse
{
    public class CancelCoursePublishRequestCommandHandler : IRequestHandler<CancelCoursePublishRequestCommand>
    {
        private readonly ICoursePublishRequestRepository _coursePublishRequestRepository;
        public CancelCoursePublishRequestCommandHandler(ICoursePublishRequestRepository coursePublishRequestRepository)
        {
            _coursePublishRequestRepository = coursePublishRequestRepository;
        }
        public async Task<Unit> Handle(CancelCoursePublishRequestCommand request, CancellationToken cancellationToken)
        {
            var coursePublishRequest = await _coursePublishRequestRepository.GetByIdAsync(request.Id);

            if (coursePublishRequest == null)
                throw new NotFoundException(nameof(CoursePublishRequest), request.Id);

            await _coursePublishRequestRepository.UpdateStatusAsync(coursePublishRequest, RequestStatus.Canceled);

            return Unit.Value;
        }
    }
}
