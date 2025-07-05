using AutoMapper;
using MediatR;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Exceptions;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Features.Lessons.Commands.UpdateLesson
{
    public class UpdateLessonCommandHandler : IRequestHandler<UpdateLessonCommand>
    {
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<Lesson> _lessonRepository;
        public UpdateLessonCommandHandler(IMapper mapper, IAsyncRepository<Lesson> lessonRepository)
        {
            _lessonRepository = lessonRepository;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(UpdateLessonCommand request, CancellationToken cancellationToken)
        {
            var lessonToUpdate = await _lessonRepository.GetByIdAsync(request.Id);

            if (lessonToUpdate == null)
                throw new NotFoundException(nameof(Lesson), request.Id);

            _mapper.Map(request, lessonToUpdate, typeof(UpdateLessonCommand), typeof(Lesson));

            await _lessonRepository.UpdateAsync(lessonToUpdate);

            return Unit.Value;
        }
    }
}