using AutoMapper;
using MediatR;
using OnlineCoursePlatform.Application.Contracts.Persistance;

namespace OnlineCoursePlatform.Application.Features.Courses.Queries.GetCoursesByTeacher
{
    public class GetCoursesByTeacherQueryHandler : IRequestHandler<GetCoursesByTeacherQuery, List<TeacherCourseDetailVm>>
    {
        private readonly IMapper _mapper;
        private readonly ICourseRepository _courseRepository;

        public GetCoursesByTeacherQueryHandler(IMapper mapper, ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
            _mapper = mapper;
        }
        public async Task<List<TeacherCourseDetailVm>> Handle(GetCoursesByTeacherQuery request, CancellationToken cancellationToken)
        {
            var courses = await _courseRepository.GetCoursesByUserIdAsync(request.UserId);

            return _mapper.Map<List<TeacherCourseDetailVm>>(courses);
        }
    }
}
