using AutoMapper;
using OnlineCoursePlatform.Application.Common.Handlers.Commands;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Features.TeacherApplications.Commands.CreateTeacherApplication
{
    public class CreateTeacherApplicationCommandHandler : CreateEntityCommandHandler<CreateTeacherApplicationCommand, TeacherApplication, Guid>
    {
        public CreateTeacherApplicationCommandHandler(IAsyncRepository<TeacherApplication> repository, IMapper mapper) : base(repository, mapper)
        {
        }

        protected override Guid BuildResponse(TeacherApplication entity) => entity.Id;
    }
}
