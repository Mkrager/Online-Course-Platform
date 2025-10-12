using OnlineCoursePlatform.Domain.Common;
using OnlineCoursePlatform.Domain.Enums;

namespace OnlineCoursePlatform.Application.Contracts.Persistance
{
    public interface IRequestRepository<T> : IAsyncRepository<T> where T : RequestEntity
    {
        Task<List<T>> GetRequestByUserIdAsync(string userId);
        Task<List<T>> GetRequestsByStatusAsync(RequestStatus? status);
        Task UpdateStatusAsync(T request, RequestStatus newStatus, string? rejectReason = null);
        Task<List<T>> GetRequestsByUserIdAndStatusAsync(string userId, RequestStatus status);
    }
}