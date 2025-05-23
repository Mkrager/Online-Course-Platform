﻿namespace OnlineCoursePlatform.Application.Features.Lessons.Queries.GetCourseLessonsList
{
    public class CourseLessonListVm
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Guid CourseId { get; set; }
        public string VideoUrl { get; set; } = string.Empty;
        public int Order { get; set; }
        public TimeSpan Duration { get; set; }
    }
}
