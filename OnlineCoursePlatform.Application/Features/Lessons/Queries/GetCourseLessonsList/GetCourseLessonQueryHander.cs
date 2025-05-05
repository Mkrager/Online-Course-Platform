using AutoMapper;
using MediatR;
using OnlineCoursePlatform.Application.Contracts.Persistance;

namespace OnlineCoursePlatform.Application.Features.Lessons.Queries.GetCourseLessonsList
{
    public class GetCourseLessonQueryHander : IRequestHandler<GetCourseLessonQuery, List<CourseLessonListVm>>
    {
        private readonly ILessonRepository _lessonRepository;
        private readonly IMapper _mapper;
        public GetCourseLessonQueryHander(ILessonRepository lessonRepository, IMapper mapper)
        {
            _lessonRepository = lessonRepository;
            _mapper = mapper;
        }
        public async Task<List<CourseLessonListVm>> Handle(GetCourseLessonQuery request, CancellationToken cancellationToken)
        {
            var lessons = await _lessonRepository.GetCourseLessons(request.CourseId);
            return _mapper.Map<List<CourseLessonListVm>>(lessons);
        }
    }
}
