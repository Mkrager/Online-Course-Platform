using AutoMapper;
using OnlineCoursePlatform.Application.Contracts.Application;
using OnlineCoursePlatform.Application.Contracts.Identity;
using OnlineCoursePlatform.Domain.Common;

namespace OnlineCoursePlatform.Application.Services
{
    public class RequestUserNamePopulator : IRequestUserNamePopulator
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public RequestUserNamePopulator(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<List<TViewModel>> PopulateUserNamesAsync<TEntity, TViewModel>(List<TEntity> requests)
            where TEntity : RequestEntity
            where TViewModel : class, new()
        {
            var userIds = requests
                .SelectMany(r => new[] { r.RequestedBy, r.ProcessedBy })
                .Where(id => !string.IsNullOrEmpty(id))
                .Distinct();

            var userNamesMap = await _userService.GetUserNamesByIdsAsync(userIds);

            var mapped = _mapper.Map<List<TViewModel>>(requests);

            for (int i = 0; i < mapped.Count; i++)
            {
                var entity = requests[i];
                dynamic vm = mapped[i];

                vm.RequestedName = userNamesMap.ContainsKey(entity.RequestedBy)
                    ? userNamesMap[entity.RequestedBy]
                    : entity.RequestedBy;

                if (!string.IsNullOrEmpty(entity.ProcessedBy))
                {
                    vm.ProcessedName = userNamesMap.ContainsKey(entity.ProcessedBy)
                        ? userNamesMap[entity.ProcessedBy]
                        : entity.ProcessedBy;
                }
            }

            return mapped;
        }
    }
}