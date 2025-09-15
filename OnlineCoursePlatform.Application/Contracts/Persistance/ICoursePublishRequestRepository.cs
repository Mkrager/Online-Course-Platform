using OnlineCoursePlatform.Domain.Entities;
using OnlineCoursePlatform.Domain.Enums;

namespace OnlineCoursePlatform.Application.Contracts.Persistance
{
    public interface ICoursePublishRequestRepository : IAsyncRepository<CoursePublishRequest>
    {
        Task<List<CoursePublishRequest>> GetCoursePublishRequestByUserIdAsync(string userId);
        Task<List<CoursePublishRequest>> GetCoursePublishRequestsAsync(CoursePublishStatus? status);
        Task UpdateStatusAsync(CoursePublishRequest coursePublishRequest, CoursePublishStatus newStatus, string? rejectReason = null);
    }
}