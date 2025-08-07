using AutoMapper;
using MediatR;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Features.Courses.Queries.GetCoursesList;

namespace OnlineCoursePlatform.Application.Features.Courses.Queries.GetCoursesByTeacher
{
    public class GetCoursesByTeacherQueryHandler : IRequestHandler<GetCoursesByTeacherQuery, List<CourseListVm>>
    {
        private readonly IMapper _mapper;
        private readonly ICourseRepository _courseRepository;

        public GetCoursesByTeacherQueryHandler(IMapper mapper, ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
            _mapper = mapper;
        }
        public async Task<List<CourseListVm>> Handle(GetCoursesByTeacherQuery request, CancellationToken cancellationToken)
        {
            var courses = await _courseRepository.GetCoursesByUserId(request.UserId);

            return _mapper.Map<List<CourseListVm>>(courses);
        }
    }
}
