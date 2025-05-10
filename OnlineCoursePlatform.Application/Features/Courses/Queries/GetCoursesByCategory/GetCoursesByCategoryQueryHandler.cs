using AutoMapper;
using MediatR;
using OnlineCoursePlatform.Application.Contracts.Persistance;

namespace OnlineCoursePlatform.Application.Features.Courses.Queries.GetCoursesByCategory
{
    public class GetCoursesByCategoryQueryHandler : IRequestHandler<GetCoursesByCategoryQuery, List<CoursesByCategoryVm>>
    {
        private readonly IMapper _mapper;
        private readonly ICourseRepository _courseRepository;

        public GetCoursesByCategoryQueryHandler(IMapper mapper, ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
            _mapper = mapper;
        }

        public async Task<List<CoursesByCategoryVm>> Handle(GetCoursesByCategoryQuery request, CancellationToken cancellationToken)
        {

            return new List<CoursesByCategoryVm>();
        }
    }
}
