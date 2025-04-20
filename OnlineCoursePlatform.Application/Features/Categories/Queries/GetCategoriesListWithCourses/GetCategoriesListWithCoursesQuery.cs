using MediatR;

namespace OnlineCoursePlatform.Application.Features.Categories.Queries.GetCategoriesListWithCourses
{
    public class GetCategoriesListWithCoursesQuery : IRequest<List<CategoryCourseListVm>>
    {
    }
}
