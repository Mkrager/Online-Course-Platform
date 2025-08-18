using MediatR;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Exceptions;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Features.Courses.Commands.UnPublishCourse
{
    public class UnPublishCourseCommandHandler : IRequestHandler<UnPublishCourseCommand>
    {
        private readonly ICourseRepository _coursesRepository;
        public UnPublishCourseCommandHandler(ICourseRepository coursesRepository)
        {
            _coursesRepository = coursesRepository;
        }
        public async Task<Unit> Handle(UnPublishCourseCommand request, CancellationToken cancellationToken)
        {
            var course = await _coursesRepository.GetByIdAsync(request.Id);

            if (course == null)
                throw new NotFoundException(nameof(Course), request.Id);

            await _coursesRepository.UpdateIsPublishedAsync(course, false);

            return Unit.Value;
        }
    }
}
