using MediatR;

namespace OnlineCoursePlatform.Application.Features.Lessons.Queries.GetCourseLessonsList
{
    public class GetCourseLessonQuery : IRequest<List<CourseLessonListVm>>
    {
        public Guid CourseId { get; set; }
    }
}
