namespace OnlineCoursePlatform.Application.Common.Filters
{
    public class CourseFilter
    {
        public Guid? CategoryId { get; set; }
        public Guid? LessonId { get; set; }
        public Guid? TestId { get; set; }
    }
}
