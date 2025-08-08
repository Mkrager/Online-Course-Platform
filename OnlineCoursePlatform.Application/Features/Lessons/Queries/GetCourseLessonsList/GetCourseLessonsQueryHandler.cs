using AutoMapper;
using MediatR;
using OnlineCoursePlatform.Application.Contracts.Persistance;

namespace OnlineCoursePlatform.Application.Features.Lessons.Queries.GetCourseLessonsList
{
    public class GetCourseLessonsQueryHandler : IRequestHandler<GetCourseLessonsQuery, List<CourseLessonListVm>>
    {
        private readonly ILessonRepository _lessonRepository;
        private readonly IMapper _mapper;
        public GetCourseLessonsQueryHandler(ILessonRepository lessonRepository, IMapper mapper)
        {
            _lessonRepository = lessonRepository;
            _mapper = mapper;
        }
        public async Task<List<CourseLessonListVm>> Handle(GetCourseLessonsQuery request, CancellationToken cancellationToken)
        {
            var lessons = await _lessonRepository.GetLessonsByCourseIdAsync(request.CourseId);
            return _mapper.Map<List<CourseLessonListVm>>(lessons);
        }
    }
}
