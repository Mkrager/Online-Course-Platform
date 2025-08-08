using AutoMapper;
using MediatR;
using OnlineCoursePlatform.Application.Contracts.Persistance;

namespace OnlineCoursePlatform.Application.Features.Enrollments.Queries.GetEnrollmentsByStudent
{
    public class GetEnrollmentsByStudentQueryHandler : IRequestHandler<GetEnrollmentsByStudentQuery, List<StudentEnrollmentsListVm>>
    {
        private readonly IMapper _mapper;
        private readonly IEnrollmentRepository _enrollmentRepository;

        public GetEnrollmentsByStudentQueryHandler(IMapper mapper, IEnrollmentRepository enrollmentRepository)
        {
            _enrollmentRepository = enrollmentRepository;
            _mapper = mapper;
        }
        public async Task<List<StudentEnrollmentsListVm>> Handle(GetEnrollmentsByStudentQuery request, CancellationToken cancellationToken)
        {
            var enrollemts = await _enrollmentRepository.GetStudentEnrollmentsWithCoursesAsync(request.UserId);

            return _mapper.Map<List<StudentEnrollmentsListVm>>(enrollemts);
        }
    }
}
