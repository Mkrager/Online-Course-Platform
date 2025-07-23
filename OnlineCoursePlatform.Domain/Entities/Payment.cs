using OnlineCoursePlatform.Domain.Common;
using OnlineCoursePlatform.Domain.Enums;

namespace OnlineCoursePlatform.Domain.Entities
{
    public class Payment : TimestampedEntity
    {
        public Guid CourseId { get; set; }
        public string PayPalOrderId { get; set; } = string.Empty;
        public string PayerId { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public OrderStatus Status { get; set; } = OrderStatus.Created;

        public Course Course { get; set; } = default!;
    }
}
