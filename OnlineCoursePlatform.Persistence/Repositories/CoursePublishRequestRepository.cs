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

        public async Task UpdateStatusAsync(CoursePublishRequest coursePublishRequest, CoursePublishStatus newStatus, string? rejectReason = null)
        {
            coursePublishRequest.Status = newStatus;
            coursePublishRequest.RejectReason = rejectReason;
            await _dbContext.SaveChangesAsync();
        }
    }
}
