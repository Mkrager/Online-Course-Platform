using AutoMapper;
using OnlineCoursePlatform.Application.Common.Handlers;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Features.Tests.Commands.CreateTest
{
    public class CreateTestCommandHandler : CreateEntityCommandHandler<CreateTestCommand, Test, Guid>
    {
        public CreateTestCommandHandler(IAsyncRepository<Test> repository, IMapper mapper) : base(repository, mapper)
        {
        }

        protected override Guid BuildResponse(Test entity) => entity.Id;
    }
}
