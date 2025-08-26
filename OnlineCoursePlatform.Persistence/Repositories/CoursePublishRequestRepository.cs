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

        public async Task UpdateStatusAsync(CoursePublishRequest coursePublishRequest, CoursePublishStatus newStatus)
        {
            coursePublishRequest.Status = newStatus;
            await _dbContext.SaveChangesAsync();
        }
    }
}
