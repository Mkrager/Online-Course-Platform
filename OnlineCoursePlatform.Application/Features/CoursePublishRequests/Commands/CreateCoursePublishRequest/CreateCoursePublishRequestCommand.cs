using MediatR;
using OnlineCoursePlatform.Application.Common.Interfaces;

namespace OnlineCoursePlatform.Application.Features.CoursePublishRequests.Commands.CreateCoursePublishRequest
{
    public class CreateCoursePublishRequestCommand : IRequest<Guid>, IUserRequest
    {
        public Guid CourseId { get; set; }
        public string? UserId { get; set; }
        public List<string> UserRoles { get; set; }
    }
}