using MediatR;
using OnlineCoursePlatform.Application.Common.Interfaces;

namespace OnlineCoursePlatform.Application.Features.Courses.Commands.UnPublishCourse
{
    public class UnPublishCourseCommand : IRequest, IUserRequest
    {
        public Guid Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public List<string> UserRoles { get; set; } = default!;
    }
}
