using MediatR;
using OnlineCoursePlatform.Application.Features.TeacherApplications.Queries.GetTeacherApplicationLIst;

namespace OnlineCoursePlatform.Application.Features.TeacherApplications.Queries.GetTeacherApplicationByUser
{
    public class GetTeacherApplicationByUserQuery : IRequest<List<TeacherApplicationListVm>>
    {
        public string UserId { get; set; } = string.Empty;
    }
}
