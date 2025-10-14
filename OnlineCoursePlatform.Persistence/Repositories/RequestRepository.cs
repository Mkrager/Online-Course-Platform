using Microsoft.EntityFrameworkCore;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Domain.Common;
using OnlineCoursePlatform.Domain.Enums;

namespace OnlineCoursePlatform.Persistence.Repositories
{
    public class RequestRepository<T> : BaseRepository<T>, IRequestRepository<T> where T : RequestEntity
    {
        public RequestRepository(OnlineCoursePlatformDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<T>> GetRequestByUserIdAsync(string userId)
        {
            return await _dbContext.Set<T>()
                .Where(r => r.CreatedBy == userId)
                .ToListAsync();
        }

        public async Task<List<T>> GetRequestsByUserIdAndStatusAsync(string userId, RequestStatus status)
        {
            return await _dbContext.Set<T>()
                .Where(r => r.CreatedBy == userId && r.Status == status)
                .ToListAsync();
        }

        public async Task<List<T>> GetRequestsByStatusAsync(RequestStatus? status)
        {
            var query = _dbContext.Set<T>().AsQueryable();

            if (status.HasValue)
            {
                query = query.Where(r => r.Status == status.Value);
            }

            return await query.ToListAsync();
        }

        public async Task UpdateStatusAsync(T request, RequestStatus newStatus, string? rejectReason = null)
        {
            request.Status = newStatus;
            request.RejectReason = rejectReason;
            await _dbContext.SaveChangesAsync();
        }
    }
}