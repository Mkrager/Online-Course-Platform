using MediatR;
using OnlineCoursePlatform.Application.Features.Courses.Queries.GetCoursesList;

namespace OnlineCoursePlatform.Application.Features.Enrollments.Queries.GetEnrollmentsByStudent
{
    public class GetEnrollmentsByStudentQuery : IRequest<List<CourseListVm>>
    {
        public string UserId { get; set; } = string.Empty;
    }
}
