using AutoMapper;
using OnlineCoursePlatform.Application.Common.Handlers.Commands;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Features.Enrollments.Commands.CreateEnrollment
{
    public class CreateEnrollmentCommandHandler : CreateEntityCommandHandler<CreateEnrollmentCommand, Enrollment, Guid>
    {
        public CreateEnrollmentCommandHandler(IAsyncRepository<Enrollment> repository, IMapper mapper) : base(repository, mapper)
        {
        }

        protected override Guid BuildResponse(Enrollment entity) => entity.Id;
    }
}
