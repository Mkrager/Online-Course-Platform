using AutoMapper;
using MediatR;
using OnlineCoursePlatform.Application.Common.Handlers.Base;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Exceptions;
using OnlineCoursePlatform.Domain.Common;

namespace OnlineCoursePlatform.Application.Common.Handlers
{
    public abstract class UpdateEntityCommandHandler<TCommand, TEntity>
        : MappedEntityCommandHandler<TEntity>, IRequestHandler<TCommand, Unit>
        where TCommand : IRequest<Unit>
        where TEntity : BaseEntity

    {
        protected UpdateEntityCommandHandler(IAsyncRepository<TEntity> repository, IMapper mapper) : base(repository, mapper)
        {
        }

        public async Task<Unit> Handle(TCommand request, CancellationToken cancellationToken)
        {
            await BeforeMapAsync(request);

            var entity = await _repository.GetByIdAsync(GetEntityId(request));
            if (entity == null)
                throw new NotFoundException(typeof(TEntity).Name, GetEntityId(request));

            _mapper.Map(request, entity);

            await BeforeUpdateAsync(entity);

            await _repository.UpdateAsync(entity);

            await AfterUpdateAsync(entity);

            return Unit.Value;
        }

        protected virtual Task BeforeMapAsync(TCommand request) => Task.CompletedTask;
        protected virtual Task BeforeUpdateAsync(TEntity entity) => Task.CompletedTask;
        protected virtual Task AfterUpdateAsync(TEntity entity) => Task.CompletedTask;

        protected abstract Guid GetEntityId(TCommand command);
    }
}