using AutoMapper;
using MediatR;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Exceptions;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Features.Lessons.Commands.CreateLesson
{
    public class CreateLessonCommandHandler : IRequestHandler<CreateLessonCommand, Guid>
    {
        private readonly IAsyncRepository<Lesson> _lessonRepository;
        private readonly IMapper _mapper;

        public CreateLessonCommandHandler(IAsyncRepository<Lesson> lessonRepository, IMapper mapper)
        {
            _lessonRepository = lessonRepository;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateLessonCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateLessonCommandValidator();
            var validatorResult = await validator.ValidateAsync(request);

            if(validatorResult.Errors.Count > 0)
                throw new ValidationException(validatorResult);

            var @lesson = _mapper.Map<Lesson>(request);

            @lesson = await _lessonRepository.AddAsync(lesson);

            return @lesson.Id;
        }
    }
}
