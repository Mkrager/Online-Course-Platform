using MediatR;

namespace OnlineCoursePlatform.Application.Features.Courses.Queries.GetCoursesByTeacher
{
    public class GetCoursesByTeacherQuery : IRequest<List<TeacherCourseDetailVm>>
    {
        public string UserId { get; set; } = string.Empty;
    }
}
