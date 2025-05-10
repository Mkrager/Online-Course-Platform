using MediatR;

namespace OnlineCoursePlatform.Application.Features.Courses.Queries.GetCoursesByCategory
{
    public class GetCoursesByCategoryQuery : IRequest<List<CoursesByCategoryVm>>
    {
        public Guid CategoryId { get; set; }
    }
}
