using MediatR;

namespace OnlineCoursePlatform.Application.Features.CoursePublishRequests.Commands.CreateCoursePublishRequest
{
    public class CreateCoursePublishRequestCommand : IRequest<Guid>
    {
        public Guid CourseId { get; set; }
        public string UserId { get; set; } = string.Empty;
    }
}
