using AutoMapper;
using MediatR;
using OnlineCoursePlatform.Application.Common.Handlers.Base;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Exceptions;
using OnlineCoursePlatform.Domain.Common;

namespace OnlineCoursePlatform.Application.Common.Handlers.Queries
{
    public abstract class GetEntityByIdQueryHandler<TQuery, TEntity, TResponse>
        : MappedEntityCommandHandler<TEntity>, IRequestHandler<TQuery, TResponse>
        where TQuery : IRequest<TResponse>
        where TEntity : BaseEntity
    {
        protected GetEntityByIdQueryHandler(
            IAsyncRepository<TEntity> repository,
            IMapper mapper) : base(repository, mapper)
        {
        }

        public async Task<TResponse> Handle(TQuery request, CancellationToken cancellationToken)
        {
            await BeforeGetAsync(request);

            var entity = await _repository.GetByIdAsync(GetEntityId(request));

            if (entity == null)
                throw new NotFoundException(typeof(TEntity).Name, GetEntityId(request));

            await AfterGetAsync(entity);

            await BeforeMapAsync(entity);

            var response = _mapper.Map<TResponse>(entity);

            await AfterMapAsync(response);

            return response;
        }

        protected virtual Task BeforeGetAsync(TQuery request) => Task.CompletedTask;
        protected virtual Task AfterGetAsync(TEntity entity) => Task.CompletedTask;
        protected virtual Task BeforeMapAsync(TEntity entity) => Task.CompletedTask;
        protected virtual Task AfterMapAsync(TResponse response) => Task.CompletedTask;

        protected abstract Guid GetEntityId(TQuery query);
    }
}