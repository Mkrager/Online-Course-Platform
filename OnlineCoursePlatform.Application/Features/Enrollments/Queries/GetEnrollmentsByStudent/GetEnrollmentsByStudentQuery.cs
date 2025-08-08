using MediatR;

namespace OnlineCoursePlatform.Application.Features.Enrollments.Queries.GetEnrollmentsByStudent
{
    public class GetEnrollmentsByStudentQuery : IRequest<List<StudentEnrollmentsListVm>>
    {
        public string UserId { get; set; } = string.Empty;
    }
}
