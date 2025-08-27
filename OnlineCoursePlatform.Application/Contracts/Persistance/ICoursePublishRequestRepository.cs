using OnlineCoursePlatform.Domain.Entities;
using OnlineCoursePlatform.Domain.Enums;

namespace OnlineCoursePlatform.Application.Contracts.Persistance
{
    public interface ICoursePublishRequestRepository : IAsyncRepository<CoursePublishRequest>
    {
        Task UpdateStatusAsync(CoursePublishRequest coursePublishRequest, CoursePublishStatus newStatus);
    }
}
