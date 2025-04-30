using MediatR;

namespace OnlineCoursePlatform.Application.Features.Levels.Queries.GetLevelsList
{
    public class GetLevelsListQuery : IRequest<List<LevelListVm>>
    {
    }
}
