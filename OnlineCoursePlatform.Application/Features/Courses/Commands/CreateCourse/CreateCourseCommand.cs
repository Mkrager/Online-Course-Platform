using MediatR;

namespace OnlineCoursePlatform.Application.Features.Courses.Commands.CreateCourse
{
    public class CreateCourseCommand : IRequest<Guid>
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Guid CategoryId { get; set; }
        public decimal Price { get; set; }
        public string ThumbnailUrl { get; set; } = string.Empty;      
    }
}
