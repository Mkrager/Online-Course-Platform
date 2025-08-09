using OnlineCoursePlatform.App.ViewModels.Course;

namespace OnlineCoursePlatform.App.ViewModels.Enrollments
{
    public class StudentEnrollmentsListViewModel
    {
        public Guid Id { get; set; }
        public int Progress { get; set; }

        public CourseListViewModel Course { get; set; } = default!;
    }
}
