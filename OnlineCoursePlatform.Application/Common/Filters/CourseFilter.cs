namespace OnlineCoursePlatform.Application.Common.Filters
{
    public class CourseFilter
    {
        public Guid? LessonId { get; set; }
        public Guid? TestId { get; set; }
        public Guid? CoursePublishRequestId { get; set; }
    }
}