using AutoMapper;
using MediatR;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Exceptions;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Features.Lessons.Commands.DeleteLesson
{
    public class DeleteLessonCommandHandler : IRequestHandler<DeleteLessonCommand>
    {
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<Lesson> _lessonRepository;

        public DeleteLessonCommandHandler(IMapper mapper, IAsyncRepository<Lesson> lessonRepository)
        {
            _lessonRepository = lessonRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteLessonCommand request, CancellationToken cancellationToken)
        {
            var lessonToDelete = await _lessonRepository.GetByIdAsync(request.Id);

            if (lessonToDelete == null)
                throw new NotFoundException(nameof(Lesson), request.Id);

            await _lessonRepository.DeleteAsync(lessonToDelete);

            return Unit.Value;
        }
    }
}
