using AutoMapper;
using MediatR;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Features.Courses.Queries.GetCoursesList
{
    public class GetCoursesListQueryHandler : IRequestHandler<GetCoursesListQuery, List<CourseListVm>>
    {
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<Course> _courseRepository;
        public GetCoursesListQueryHandler(IMapper mapper, IAsyncRepository<Course> courseRepository)
        {
            _courseRepository = courseRepository;
            _mapper = mapper;
        }

        public async Task<List<CourseListVm>> Handle(GetCoursesListQuery request, CancellationToken cancellationToken)
        {
            var allCourses = (await _courseRepository.ListAllAsync()).OrderBy(x => x.CreatedDate);
            return _mapper.Map<List<CourseListVm>>(allCourses);
        }
    }
}
