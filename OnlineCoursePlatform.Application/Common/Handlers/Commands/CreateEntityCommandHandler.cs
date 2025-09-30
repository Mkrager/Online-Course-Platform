using AutoMapper;
using MediatR;
using OnlineCoursePlatform.Application.Common.Handlers.Base;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Domain.Common;

namespace OnlineCoursePlatform.Application.Common.Handlers.Commands
{
    public abstract class CreateEntityCommandHandler<TCommand, TEntity, TResponse>
        : MappedEntityCommandHandler<TEntity>, IRequestHandler<TCommand, TResponse>
        where TCommand : IRequest<TResponse>
        where TEntity : BaseEntity
    {
        protected CreateEntityCommandHandler(
            IAsyncRepository<TEntity> repository, 
            IMapper mapper) 
            : base(repository, mapper)
        {
        }

        public async Task<TResponse> Handle(TCommand request, CancellationToken cancellationToken)
        {
            await BeforeMapAsync(request);

            var entity = _mapper.Map<TEntity>(request);

            await BeforeSaveAsync(entity);

            entity = await _repository.AddAsync(entity);

            await AfterSaveAsync(entity);

            return BuildResponse(entity);
        }

        protected virtual Task BeforeMapAsync(TCommand request) => Task.CompletedTask;
        protected virtual Task BeforeSaveAsync(TEntity entity) => Task.CompletedTask;
        protected virtual Task AfterSaveAsync(TEntity entity) => Task.CompletedTask;

        protected abstract TResponse BuildResponse(TEntity entity);
    }
}