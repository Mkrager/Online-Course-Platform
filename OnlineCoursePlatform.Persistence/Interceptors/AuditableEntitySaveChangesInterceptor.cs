using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using OnlineCoursePlatform.Application.Contracts;
using OnlineCoursePlatform.Domain.Common.Interfaces;
using OnlineCoursePlatform.Domain.Common;
using OnlineCoursePlatform.Domain.Entities;
using OnlineCoursePlatform.Domain.Enums;

namespace OnlineCoursePlatform.Persistence.Interceptors
{
    public class AuditableEntitySaveChangesInterceptor : SaveChangesInterceptor
    {
        private readonly ICurrentUserService _currentUserService;

        public AuditableEntitySaveChangesInterceptor(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
        {
            var context = eventData.Context;

            if (context == null)
                return base.SavingChangesAsync(eventData, result, cancellationToken);

            var now = DateTime.UtcNow;
            var userId = _currentUserService.UserId;

            foreach (var entry in context.ChangeTracker.Entries())
            {
                if (entry.Entity is AuditableEntity auditable)
                {
                    if (entry.State == EntityState.Added)
                    {
                        auditable.CreatedDate = now;
                        auditable.CreatedBy = userId;
                    }
                    else if (entry.State == EntityState.Modified)
                    {
                        auditable.LastModifiedDate = now;
                        auditable.LastModifiedBy = userId;
                    }
                }

                if (entry.Entity is TimestampedEntity timestamped)
                {
                    if (entry.State == EntityState.Added)
                        timestamped.CreatedAt = now;
                    else if (entry.State == EntityState.Modified)
                        timestamped.LastModifiedDate = now;
                }

                if (entry.Entity is IHasUserId userEntity && entry.State == EntityState.Added)
                {
                    userEntity.UserId = userId;
                }

                if (entry.Entity is RequestEntity teacherApplication)
                {
                    if (entry.State == EntityState.Added)
                    {
                        teacherApplication.RequestedBy = userId;
                        teacherApplication.RequestedDate = now;
                        teacherApplication.Status = RequestStatus.Pending;
                    }
                    else if (entry.State == EntityState.Modified)
                    {
                        teacherApplication.ProcessedBy = userId;
                        teacherApplication.ProcessedAt = now;
                    }
                }
            }

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }
}