using MediatR;
using OnlineCoursePlatform.Application.Common.Interfaces;

namespace OnlineCoursePlatform.Application.Features.Lessons.Queries.GetCourseLessonsList
{
    public class GetCourseLessonsQuery : IRequest<List<CourseLessonListVm>>, IUserRequest
    {
        public Guid CourseId { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string UserRoles { get; set; } = string.Empty;
    }
}
