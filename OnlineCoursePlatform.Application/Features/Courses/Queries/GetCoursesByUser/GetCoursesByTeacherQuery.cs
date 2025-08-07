using MediatR;

namespace OnlineCoursePlatform.Application.Features.Courses.Queries.GetCoursesByUser
{
    public class GetCoursesByTeacherQuery : IRequest<List<CourseByTeacherVm>>
    {
        public string UserId { get; set; } = string.Empty;
    }
}
