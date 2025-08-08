using AutoMapper;
using MediatR;
using OnlineCoursePlatform.Application.Contracts.Persistance;

namespace OnlineCoursePlatform.Application.Features.Categories.Queries.GetCategoriesListWithCourses
{
    public class GetCategoriesListWithCoursesQueryHandler : IRequestHandler<GetCategoriesListWithCoursesQuery, List<CategoryCourseListVm>>
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;
        public GetCategoriesListWithCoursesQueryHandler(IMapper mapper, ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<List<CategoryCourseListVm>> Handle(GetCategoriesListWithCoursesQuery request, CancellationToken cancellationToken)
        {
            var list = await _categoryRepository.GetCategoriesWithCoursesAsync();
            return _mapper.Map<List<CategoryCourseListVm>>(list);
        }
    }
}
