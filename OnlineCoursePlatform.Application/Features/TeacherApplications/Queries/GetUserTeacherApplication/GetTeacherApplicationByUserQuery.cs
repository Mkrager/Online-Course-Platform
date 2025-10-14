using MediatR;
using OnlineCoursePlatform.Application.Common.Interfaces;
using OnlineCoursePlatform.Application.Features.TeacherApplications.Queries.GetTeacherApplicationLIst;

namespace OnlineCoursePlatform.Application.Features.TeacherApplications.Queries.GetUserTeacherApplication
{
    public class GetTeacherApplicationByUserQuery : IRequest<List<TeacherApplicationListVm>>, IUserIdRequest
    {
        public string? UserId { get; set; }
    }
}
