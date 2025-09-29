using AutoMapper;
using MediatR;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Domain.Common;

namespace OnlineCoursePlatform.Application.Common.Handlers
{
    public abstract class CreateEntityCommandHandler<TCommand, TEntity, TResponse>
        : IRequestHandler<TCommand, TResponse>
        where TCommand : IRequest<TResponse>
        where TEntity : BaseEntity
    {
        private readonly IAsyncRepository<TEntity> _repository;
        private readonly IMapper _mapper;

        protected CreateEntityCommandHandler(
            IAsyncRepository<TEntity> repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
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