using OnlineCoursePlatform.Application.Features.Courses.Queries.GetCoursesList;

namespace OnlineCoursePlatform.Application.Features.Enrollments.Queries.GetEnrollmentsByStudent
{
    public class StudentEnrollmentsListVm
    {
        public Guid Id { get; set; }
        public int Progress { get; set; }

        public CourseListVm Course { get; set; } = default!;
    }
}
