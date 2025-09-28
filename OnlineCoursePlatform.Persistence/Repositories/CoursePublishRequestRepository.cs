using Microsoft.EntityFrameworkCore;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Domain.Entities;
using OnlineCoursePlatform.Domain.Enums;

namespace OnlineCoursePlatform.Persistence.Repositories
{
    public class CoursePublishRequestRepository : BaseRepository<CoursePublishRequest>, ICoursePublishRequestRepository
    {
        public CoursePublishRequestRepository(OnlineCoursePlatformDbContext dbContext) : base(dbContext)
        {      
        }

        public async Task<List<CoursePublishRequest>> GetCoursePublishRequestByUserIdAsync(string userId)
        {
            return await _dbContext.CoursePublishRequests
                .Where(r => r.CreatedBy == userId)
                .ToListAsync();
        }

        public async Task<List<CoursePublishRequest>> GetCoursePublishRequestsAsync(RequestStatus? status)
        {
            var query = _dbContext.CoursePublishRequests.AsQueryable();

            if (status.HasValue)
            {
                query = query.Where(r => r.Status == status.Value);
            }

            return await query.ToListAsync();
        }

        public async Task UpdateStatusAsync(CoursePublishRequest coursePublishRequest, RequestStatus newStatus, string? rejectReason = null)
        {
            coursePublishRequest.Status = newStatus;
            coursePublishRequest.RejectReason = rejectReason;
            await _dbContext.SaveChangesAsync();
        }
    }
}
