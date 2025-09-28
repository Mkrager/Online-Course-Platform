using MediatR;
using OnlineCoursePlatform.Application.Common.Interfaces;

namespace OnlineCoursePlatform.Application.Features.Enrollments.Queries.GetEnrollmentsByStudent
{
    public class GetEnrollmentsByStudentQuery : IRequest<List<StudentEnrollmentsListVm>>, IUserIdRequest
    {
        public string? UserId { get; set; }
    }
}
