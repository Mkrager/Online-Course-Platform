using AutoMapper;
using MediatR;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Features.Levels.Queries.GetLevelsList
{
    public class GetLevelsListQueryHandler : IRequestHandler<GetLevelsListQuery, List<LevelListVm>>
    {
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<Level> _levelRepository;

        public GetLevelsListQueryHandler(IMapper mapper, IAsyncRepository<Level> levelRepository)
        {
            _levelRepository = levelRepository;
            _mapper = mapper;
        }

        public async Task<List<LevelListVm>> Handle(GetLevelsListQuery request, CancellationToken cancellationToken)
        {
            var allLevels = (await _levelRepository.ListAllAsync()).OrderBy(o => o.Order);
            return _mapper.Map<List<LevelListVm>>(allLevels);
        }
    }
}
