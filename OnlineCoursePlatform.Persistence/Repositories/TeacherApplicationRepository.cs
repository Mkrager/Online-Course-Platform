using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Domain.Entities;
using OnlineCoursePlatform.Domain.Enums;

namespace OnlineCoursePlatform.Persistence.Repositories
{
    public class TeacherApplicationRepository : BaseRepository<TeacherApplication>, ITeacherApplicationRepository
    {
        public TeacherApplicationRepository(OnlineCoursePlatformDbContext dbContext) : base(dbContext)
        {
        }

        public async Task UpdateStatusAsync(TeacherApplication teacherApplication, RequestStatus newStatus, string? rejectReason = null)
        {
            teacherApplication.Status = newStatus;
            teacherApplication.RejectReason = rejectReason;
            await _dbContext.SaveChangesAsync();
        }
    }
}