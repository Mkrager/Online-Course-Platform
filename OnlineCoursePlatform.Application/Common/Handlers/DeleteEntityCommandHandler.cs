using MediatR;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Domain.Common;
using OnlineCoursePlatform.Application.Exceptions;

namespace OnlineCoursePlatform.Application.Common.Handlers
{
    public abstract class DeleteEntityCommandHandler<TCommand, TEntity>
        : IRequestHandler<TCommand, Unit>
        where TCommand : IRequest<Unit>
        where TEntity : BaseEntity
    {
        protected readonly IAsyncRepository<TEntity> _repository;

        protected DeleteEntityCommandHandler(IAsyncRepository<TEntity> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(TCommand request, CancellationToken cancellationToken)
        {
            await BeforeDeleteAsync(request);

            var entity = await _repository.GetByIdAsync(GetEntityId(request));
            if (entity == null)
                throw new NotFoundException(typeof(TEntity).Name, GetEntityId(request));

            await BeforeRemoveAsync(entity);

            await _repository.DeleteAsync(entity);

            await AfterDeleteAsync(entity);

            return Unit.Value;
        }

        protected virtual Task BeforeDeleteAsync(TCommand request) => Task.CompletedTask;
        protected virtual Task BeforeRemoveAsync(TEntity entity) => Task.CompletedTask;
        protected virtual Task AfterDeleteAsync(TEntity entity) => Task.CompletedTask;

        protected abstract Guid GetEntityId(TCommand command);
    }
}