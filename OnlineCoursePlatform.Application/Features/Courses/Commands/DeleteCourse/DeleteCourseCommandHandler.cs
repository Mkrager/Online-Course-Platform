using AutoMapper;
using MediatR;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Exceptions;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Features.Courses.Commands.DeleteCourse
{
    public class DeleteCourseCommandHandler : IRequestHandler<DeleteCourseCommand>
    {
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<Course> _courseRepository;

        public DeleteCourseCommandHandler(IMapper mapper, IAsyncRepository<Course> courseRepository)
        {
            _courseRepository = courseRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
        {
            var courseToDelete = await _courseRepository.GetByIdAsync(request.Id);

            if (courseToDelete == null)
            {
                throw new NotFoundException(nameof(Course), request.Id);
            }

            await _courseRepository.DeleteAsync(courseToDelete);

            return Unit.Value;
        }
    }
}
