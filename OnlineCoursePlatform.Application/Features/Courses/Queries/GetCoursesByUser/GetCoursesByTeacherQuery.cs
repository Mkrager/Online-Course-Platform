using MediatR;
using OnlineCoursePlatform.Application.Features.Courses.Queries.GetCoursesList;

namespace OnlineCoursePlatform.Application.Features.Courses.Queries.GetCoursesByUser
{
    public class GetCoursesByTeacherQuery : IRequest<List<CourseListVm>>
    {
        public string UserId { get; set; } = string.Empty;
    }
}
