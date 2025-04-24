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
        private readonly IAsyncRepository<Category> _categoryRepository;
        public GetCoursesListQueryHandler(IMapper mapper, IAsyncRepository<Course> courseRepository, IAsyncRepository<Category> categoryRepository)
        {
            _courseRepository = courseRepository;
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        public async Task<List<CourseListVm>> Handle(GetCoursesListQuery request, CancellationToken cancellationToken)
        {
            var allCourses = (await _courseRepository.ListAllAsync()).OrderBy(x => x.CreatedDate);

            var coursesListDto = _mapper.Map<List<CourseListVm>>(allCourses);

            var categories = await _categoryRepository.ListAllAsync();

            foreach (var course in coursesListDto)
            {
                course.Category = _mapper.Map<CategoryDto>(categories.FirstOrDefault(c => c.Id == course.CategoryId));
            }

            return coursesListDto;
        }
    }
}
