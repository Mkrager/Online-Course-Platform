using OnlineCoursePlatform.Application.Common.Interfaces;
using OnlineCoursePlatform.Application.Contracts.Application;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Domain.Common;
using OnlineCoursePlatform.Domain.Enums;

namespace OnlineCoursePlatform.Application.Common.Validators
{
    public abstract class RequestAccessValidator<TCommand, TEntity>
        : AccessValidator<TCommand, IRequestRepository<TEntity>>
        where TCommand : IUserRequest
        where TEntity : RequestEntity
    {
        protected RequestAccessValidator(IRequestRepository<TEntity> service, IPermissionService permissionService, string? errorMessage = null)
            : base(service, permissionService, errorMessage)
        {
        }

        protected async override Task<bool> HasAccessInternal(TCommand model, CancellationToken token)
        {
            var entity = await _service.GetByIdAsync(GetId(model));

            if (entity == null)
                return false;

            if (!await IsOwnerOrHasPermission(entity, model))
                return false;

            return await HasValidStatus(entity);
        }

        protected virtual Task<bool> IsOwnerOrHasPermission(TEntity entity, TCommand model)
        {
            bool isOwner = entity.RequestedBy == model.UserId;
            return Task.FromResult(isOwner);
        }

        protected virtual Task<bool> HasValidStatus(TEntity entity)
        {
            return Task.FromResult(entity.Status == RequestStatus.Pending);
        }
        protected abstract Guid GetId(TCommand request);
    }
}