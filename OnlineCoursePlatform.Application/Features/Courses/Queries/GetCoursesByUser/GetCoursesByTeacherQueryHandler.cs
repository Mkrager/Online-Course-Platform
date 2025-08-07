using AutoMapper;
using MediatR;
using OnlineCoursePlatform.Application.Contracts.Persistance;

namespace OnlineCoursePlatform.Application.Features.Courses.Queries.GetCoursesByUser
{
    public class GetCoursesByTeacherQueryHandler : IRequestHandler<GetCoursesByTeacherQuery, List<CourseByTeacherVm>>
    {
        private readonly IMapper _mapper;
        private readonly ICourseRepository _courseRepository;

        public GetCoursesByTeacherQueryHandler(IMapper mapper, ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
            _mapper = mapper;
        }
        public async Task<List<CourseByTeacherVm>> Handle(GetCoursesByTeacherQuery request, CancellationToken cancellationToken)
        {
            var courses = await _courseRepository.GetCoursesByUserId(request.UserId);

            return _mapper.Map<List<CourseByTeacherVm>>(courses);
        }
    }
}
