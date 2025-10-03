using MediatR;
using OnlineCoursePlatform.Application.Common.Interfaces;

namespace OnlineCoursePlatform.Application.Features.CoursePublishRequests.Commands.UpdateCoursePublishRequestStatus.CancelCourse
{
    public class CancelTeacherApplicationCommand : IRequest, IUserRequest
    {
        public Guid Id { get; set; }
        public string? UserId { get; set; }
        public List<string> UserRoles { get; set; } = default!;
    }
}
