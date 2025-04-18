using AutoMapper;
using MediatR;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Exceptions;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Features.Courses.Commands.UpdateCourse
{
    public class UpdateCourseCommandHandler : IRequestHandler<UpdateCourseCommand>
    {
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<Course> _courseRepository;

        public UpdateCourseCommandHandler(IMapper mapper, IAsyncRepository<Course> courseRepository)
        {
            _courseRepository = courseRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
        {
            var courseToUpdate = await _courseRepository.GetByIdAsync(request.Id);
            if (courseToUpdate == null)
            {
                throw new NotFoundException(nameof(Course), request.Id);
            }

            var validator = new UpdateCourseCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
                throw new ValidationException(validationResult);

            _mapper.Map(request, courseToUpdate, typeof(UpdateCourseCommand), typeof(Course));

            return Unit.Value;
        }
    }
}
