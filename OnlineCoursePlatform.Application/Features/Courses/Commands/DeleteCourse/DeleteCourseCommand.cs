using MediatR;

namespace OnlineCoursePlatform.Application.Features.Courses.Commands.DeleteCourse
{
    public class DeleteCourseCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
