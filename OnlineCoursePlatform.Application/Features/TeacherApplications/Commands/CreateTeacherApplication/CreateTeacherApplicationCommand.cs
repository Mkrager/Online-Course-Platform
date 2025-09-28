using MediatR;

namespace OnlineCoursePlatform.Application.Features.TeacherApplications.Commands.CreateTeacherApplication
{
    public class CreateTeacherApplicationCommand : IRequest<Guid>
    {
        public string UserId { get; set; } = string.Empty;
        public List<string> UserRoles { get; set; } = default!;
    }
}
