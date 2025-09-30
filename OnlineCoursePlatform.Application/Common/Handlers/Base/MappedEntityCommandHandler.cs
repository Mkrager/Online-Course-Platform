using AutoMapper;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Domain.Common;

namespace OnlineCoursePlatform.Application.Common.Handlers.Base
{
    public abstract class MappedEntityCommandHandler<TEntity>
        : EntityCommandHandler<TEntity>
        where TEntity : BaseEntity
    {
        protected readonly IMapper _mapper;

        protected MappedEntityCommandHandler(
            IAsyncRepository<TEntity> repository,
            IMapper mapper) : base(repository)
        {
            _mapper = mapper;
        }
    }
}