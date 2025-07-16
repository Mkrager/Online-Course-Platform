//using AutoMapper;
//using MediatR;
//using OnlineCoursePlatform.Application.Contracts.Persistance;
//using OnlineCoursePlatform.Domain.Entities;

//namespace OnlineCoursePlatform.Application.Features.Enrollments.Commands.CreateEnrollment
//{
//    public class CreateEnrollmentCommandHandler : IRequestHandler<CreateEnrollmentCommand, Guid>
//    {
//        private readonly IAsyncRepository<Enrollment> _enrollmentRepository;
//        private readonly IMapper _mapper;
//        public CreateEnrollmentCommandHandler(IAsyncRepository<Enrollment> enrollmentRepository, IMapper mapper)
//        {
//            _enrollmentRepository = enrollmentRepository;
//            _mapper = mapper;
//        }

//        public async Task<Guid> Handle(CreateEnrollmentCommand request, CancellationToken cancellationToken)
//        {
//            var enrollmentToCreate = _mapper.Map<Enrollment>(request);
//            var enrollment = await _enrollmentRepository.AddAsync(enrollmentToCreate);

//            return enrollment.Id;
//        }
//    }
//}
