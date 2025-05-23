﻿using MediatR;

namespace OnlineCoursePlatform.Application.Features.Lessons.Queries.GetCourseLessonsList
{
    public class GetCourseLessonsQuery : IRequest<List<CourseLessonListVm>>
    {
        public Guid CourseId { get; set; }
    }
}
