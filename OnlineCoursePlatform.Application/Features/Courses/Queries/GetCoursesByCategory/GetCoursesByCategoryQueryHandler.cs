using AutoMapper;
using MediatR;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Features.Courses.Queries.GetCoursesByCategory
{
    public class GetCoursesByCategoryQueryHandler : IRequestHandler<GetCoursesByCategoryQuery, List<CoursesByCategoryVm>>
    {
        private readonly IMapper _mapper;
        private readonly ICourseRepository _courseRepository;
        private readonly IAsyncRepository<Level> _levelRepository;
        private readonly IAsyncRepository<Category> _categoryRepository;

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
