using MediatR;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Exceptions;

namespace OnlineCoursePlatform.Application.Features.Courses.Commands.PublishCourse
{
    public class PublishCourseCommandHandler : IRequestHandler<PublishCourseCommand>
    {
        private readonly ICourseRepository _courseRepository;
        
        public PublishCourseCommandHandler(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task<Unit> Handle(PublishCourseCommand request, CancellationToken cancellationToken)
        {
            var course = await _courseRepository.GetByIdAsync(request.Id);

            if (course == null)
                throw new NotFoundException(nameof(course), request.Id);

            await _courseRepository.UpdateIsPublishedAsync(course, true);

            return Unit.Value;
        }
    }
}