using MediatR;

namespace OnlineCoursePlatform.Application.Features.Enrollments.Commands.CreateEnrollment
{
    public class CreateEnrollmentCommand : IRequest<Guid>
    {
        public Guid CourseId { get; set; }
        public string StudentId { get; set; } = string.Empty;
    }
}
