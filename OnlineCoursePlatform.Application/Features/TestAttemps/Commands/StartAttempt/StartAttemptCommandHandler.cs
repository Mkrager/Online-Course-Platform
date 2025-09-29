using AutoMapper;
using OnlineCoursePlatform.Application.Common.Handlers.Commands;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Features.TestAttemps.Commands.StartAttempt
{
    public class StartAttemptCommandHandler : CreateEntityCommandHandler<StartAttemptCommand, TestAttempt, Guid>
    {
        public StartAttemptCommandHandler(IAsyncRepository<TestAttempt> repository, IMapper mapper) : base(repository, mapper)
        {
        }

        protected override Task BeforeSaveAsync(TestAttempt entity)
        {
            entity.StartTime = DateTime.Now;
            return Task.CompletedTask;
        }

        protected override Guid BuildResponse(TestAttempt entity) => entity.Id;
    }
}