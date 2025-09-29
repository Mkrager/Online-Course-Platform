using OnlineCoursePlatform.Application.Common.Handlers.Commands;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Features.Tests.Commands.DeleteTest
{
    public class DeleteTestCommandHandler : DeleteEntityCommandHandler<DeleteTestCommand, Test>
    {
        public DeleteTestCommandHandler(IAsyncRepository<Test> repository) : base(repository)
        {
        }

        protected override Guid GetEntityId(DeleteTestCommand command) => command.Id;
    }
}