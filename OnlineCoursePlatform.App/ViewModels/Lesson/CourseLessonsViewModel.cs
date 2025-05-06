namespace OnlineCoursePlatform.App.ViewModels.Lesson
{
    public class CourseLessonsViewModel
    {
        public Guid CourseId { get; set; }
        public List<LessonViewModel> Lessons { get; set; }
    }
}
