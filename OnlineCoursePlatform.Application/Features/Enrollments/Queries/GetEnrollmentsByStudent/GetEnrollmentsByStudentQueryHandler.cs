using AutoMapper;
using MediatR;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Features.Courses.Queries.GetCoursesList;

namespace OnlineCoursePlatform.Application.Features.Enrollments.Queries.GetEnrollmentsByStudent
{
    public class GetEnrollmentsByStudentQueryHandler : IRequestHandler<GetEnrollmentsByStudentQuery, List<CourseListVm>>
    {
        private readonly IMapper _mapper;
        private readonly IEnrollmentRepository _enrollmentRepository;

        public GetEnrollmentsByStudentQueryHandler(IMapper mapper, IEnrollmentRepository enrollmentRepository)
        {
            _enrollmentRepository = enrollmentRepository;
            _mapper = mapper;
        }
        public async Task<List<CourseListVm>> Handle(GetEnrollmentsByStudentQuery request, CancellationToken cancellationToken)
        {
            var enrollemts = await _enrollmentRepository.GetStudentEnrollmentsWithCoursesAsync(request.UserId);

            return _mapper.Map<List<CourseListVm>>(enrollemts);
        }
    }
}
