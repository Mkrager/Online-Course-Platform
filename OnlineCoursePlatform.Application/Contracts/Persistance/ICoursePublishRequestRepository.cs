using OnlineCoursePlatform.Domain.Entities;
using OnlineCoursePlatform.Domain.Enums;

namespace OnlineCoursePlatform.Application.Contracts.Persistance
{
    public interface ICoursePublishRequestRepository : IAsyncRepository<CoursePublishRequest>
    {
        Task<List<CoursePublishRequest>> GetCoursePublishRequestByUserIdAsync(string userId);
        Task<List<CoursePublishRequest>> GetCoursePublishRequestsAsync(RequestStatus? status);
        Task UpdateStatusAsync(CoursePublishRequest coursePublishRequest, RequestStatus newStatus, string? rejectReason = null);
    }
}