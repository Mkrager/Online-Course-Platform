using OnlineCoursePlatform.Domain.Enums;

namespace OnlineCoursePlatform.Application.Features.CoursePublishRequests.Queries.GetCoursePublishRequestsList
{
    public class CoursePublishRequestsListVm
    {
        public Guid CourseId { get; set; }
        public string RequestedBy { get; set; } = string.Empty;
        public string RequestedName { get; set; } = string.Empty;
        public DateTime RequestedDate { get; set; }
        public string ApprovedBy { get; set; } = string.Empty;
        public string ApprovedName {  get; set; } = string.Empty;
        public DateTime ApprovedAt { get; set; }
        public CoursePublishStatus Status { get; set; }
    }
}
