namespace OnlineCoursePlatform.Domain.Common
{
    public abstract class TimestampedEntity : BaseEntity
    {
        public DateTime CreatedAt { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}
