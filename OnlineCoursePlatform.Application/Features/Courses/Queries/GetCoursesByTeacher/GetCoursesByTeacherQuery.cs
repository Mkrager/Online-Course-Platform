using MediatR;
using OnlineCoursePlatform.Application.Common.Interfaces;

namespace OnlineCoursePlatform.Application.Features.Courses.Queries.GetCoursesByTeacher
{
    public class GetCoursesByTeacherQuery : IRequest<List<TeacherCourseDetailVm>>, IUserIdRequest
    {
        public string? UserId { get; set; }
    }
}
