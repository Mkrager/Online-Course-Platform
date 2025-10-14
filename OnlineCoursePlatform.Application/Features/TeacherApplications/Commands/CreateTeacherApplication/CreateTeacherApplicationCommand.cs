using MediatR;
using OnlineCoursePlatform.Application.Common.Interfaces;

namespace OnlineCoursePlatform.Application.Features.TeacherApplications.Commands.CreateTeacherApplication
{
    public class CreateTeacherApplicationCommand : IRequest<Guid>, IUserIdRequest
    {
        public string Bio { get; set; } = string.Empty;
        public string Experience { get; set; } = string.Empty;

        public string? UserId { get ; set; }
    }
}
