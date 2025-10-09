using MediatR;

namespace OnlineCoursePlatform.Application.Features.TeacherApplications.Queries.GetTeacherApplicationLIst
{
    public class GetTeacherApplicationListQuery : IRequest<List<TeacherApplicationListVm>>
    {
    }
}