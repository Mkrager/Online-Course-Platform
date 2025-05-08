using AutoMapper;
using MediatR;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Exceptions;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Features.Lessons.Queries.GetLessonDetail
{
    public class GetLessonDetailQueryHandler : IRequestHandler<GetLessonDetailQuery, LessonDetailVm>
    {
        private readonly IAsyncRepository<Lesson> _lessonRepository;
        private readonly IMapper _mapper;
        public GetLessonDetailQueryHandler(IAsyncRepository<Lesson> lessonRepository, IMapper mapper)
        {
            _lessonRepository = lessonRepository;
            _mapper = mapper;
        }

        public async Task<LessonDetailVm> Handle(GetLessonDetailQuery request, CancellationToken cancellationToken)
        {
            var lesson = await _lessonRepository.GetByIdAsync(request.Id);

            if (lesson == null)
                throw new NotFoundException(nameof(Lesson), request.Id);

            return _mapper.Map<LessonDetailVm>(lesson);
        }
    }
}
