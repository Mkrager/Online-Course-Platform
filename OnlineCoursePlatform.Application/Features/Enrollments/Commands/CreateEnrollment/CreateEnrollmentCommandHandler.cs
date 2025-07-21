using AutoMapper;
using MediatR;
using OnlineCoursePlatform.Application.Contracts;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Features.Enrollments.Commands.CreateEnrollment
{
    public class CreateEnrollmentCommandHandler : IRequestHandler<CreateEnrollmentCommand, Guid>
    {
        private readonly IAsyncRepository<Enrollment> _enrollmentRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;
        public CreateEnrollmentCommandHandler(IAsyncRepository<Enrollment> enrollmentRepository, IMapper mapper, ICurrentUserService currentUserService)
        {
            _enrollmentRepository = enrollmentRepository;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<Guid> Handle(CreateEnrollmentCommand request, CancellationToken cancellationToken)
        {
            var enrollmentToCreate = _mapper.Map<Enrollment>(request);

            enrollmentToCreate.EnrolledAt = DateTime.UtcNow;

            var enrollment = await _enrollmentRepository.AddAsync(enrollmentToCreate);
            return enrollment.Id;
        }
    }
}
