using MediatR;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Exceptions;
using OnlineCoursePlatform.Domain.Entities;
using OnlineCoursePlatform.Domain.Enums;

namespace OnlineCoursePlatform.Application.Features.CoursePublishRequests.Commands.UpdateCoursePublishRequestStatus.ApproveCourse
{
    public class ApproveCoursePublishRequestCommandHandler : IRequestHandler<ApproveCoursePublishRequestCommand>
    {
        private readonly ICoursePublishRequestRepository _coursePublishRequestRepository;
        private readonly ICourseRepository _courseRepository;

        public ApproveCoursePublishRequestCommandHandler(ICoursePublishRequestRepository coursePublishRequestRepository, ICourseRepository courseRepository)
        {
            _coursePublishRequestRepository = coursePublishRequestRepository;
            _courseRepository = courseRepository;
        }
        public async Task<Unit> Handle(ApproveCoursePublishRequestCommand request, CancellationToken cancellationToken)
        {
            var coursePublishRequest = await _coursePublishRequestRepository.GetByIdAsync(request.Id);

            if (coursePublishRequest == null)
                throw new NotFoundException(nameof(CoursePublishRequest), request.Id);

            await _coursePublishRequestRepository.UpdateStatusAsync(coursePublishRequest, CoursePublishStatus.Approved);

            var course = await _courseRepository.GetByIdAsync(coursePublishRequest.CourseId);

            if (course == null)
                throw new NotFoundException(nameof(Course), coursePublishRequest.CourseId);

            await _courseRepository.UpdateIsPublishedAsync(course, true);

            return Unit.Value;
        }
    }
}