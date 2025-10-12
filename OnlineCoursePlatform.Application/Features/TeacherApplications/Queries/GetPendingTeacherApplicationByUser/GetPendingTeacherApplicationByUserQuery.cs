using MediatR;
using OnlineCoursePlatform.Application.Features.TeacherApplications.Queries.GetTeacherApplicationLIst;

namespace OnlineCoursePlatform.Application.Features.TeacherApplications.Queries.GetPendingTeacherApplicationByUser
{
    public class GetPendingTeacherApplicationByUserQuery : IRequest<List<TeacherApplicationListVm>>
    {
        public string UserId { get; set; } = string.Empty;
    }
}
