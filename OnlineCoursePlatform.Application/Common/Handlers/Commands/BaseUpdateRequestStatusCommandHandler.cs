using MediatR;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Exceptions;
using OnlineCoursePlatform.Domain.Common;

namespace OnlineCoursePlatform.Application.Common.Handlers.Commands
{
    public abstract class BaseUpdateRequestStatusCommandHandler<TEntity, TCommand>
        : IRequestHandler<TCommand>
        where TEntity : RequestEntity
        where TCommand : IRequest<Unit>
    {
        private readonly IRequestRepository<TEntity> _repository;
        protected BaseUpdateRequestStatusCommandHandler(IRequestRepository<TEntity> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(TCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(GetId(request));

            if (entity == null)
                throw new NotFoundException(typeof(TEntity).Name, GetId(request));

            await HandleRequestAsync(entity, request, cancellationToken);

            return Unit.Value;
        }

        protected abstract Guid GetId(TCommand request);

        protected abstract Task HandleRequestAsync(TEntity entity, TCommand request, CancellationToken cancellationToken);

        protected Task UpdateStatusAsync(TEntity entity, Domain.Enums.RequestStatus status, string? reason = null)
        {
            entity.Status = status;
            entity.RejectReason = reason;
            return _repository.UpdateStatusAsync(entity, status, reason);
        }
    }
}