using MediatR;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Exceptions;
using OnlineCoursePlatform.Domain.Entities;
using OnlineCoursePlatform.Domain.Enums;

namespace OnlineCoursePlatform.Application.Features.CoursePublishRequests.Commands.ApproveCourse
{
    public class ApproveCoursePublishRequestCommandHandler : IRequestHandler<ApproveCoursePublishRequestCommand>
    {
        private readonly ICoursePublishRequestRepository _coursePublishRequestRepository;
        
        public ApproveCoursePublishRequestCommandHandler(ICoursePublishRequestRepository coursePublishRequestRepository)
        {
            _coursePublishRequestRepository = coursePublishRequestRepository;
        }
        public async Task<Unit> Handle(ApproveCoursePublishRequestCommand request, CancellationToken cancellationToken)
        {
            var coursePublishRequest = await _coursePublishRequestRepository.GetByIdAsync(request.Id);

            if (coursePublishRequest == null)
                throw new NotFoundException(nameof(CoursePublishRequest), request.Id);

            await _coursePublishRequestRepository.UpdateStatusAsync(coursePublishRequest, CoursePublishStatus.Approved);

            return Unit.Value;
        }
    }
}