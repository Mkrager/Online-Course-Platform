using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Domain.Common;

namespace OnlineCoursePlatform.Application.Common.Handlers.Base
{
    public abstract class EntityCommandHandler<TEntity>
        where TEntity : BaseEntity
    {
        protected readonly IAsyncRepository<TEntity> _repository;

        protected EntityCommandHandler(IAsyncRepository<TEntity> repository)
        {
            _repository = repository;
        }
    }
}