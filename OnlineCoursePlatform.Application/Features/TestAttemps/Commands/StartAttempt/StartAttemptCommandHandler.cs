using AutoMapper;
using MediatR;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Features.TestAttemps.Commands.StartAttempt
{
    public class StartAttemptCommandHandler : IRequestHandler<StartAttemptCommand, Guid>
    {
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<TestAttempt> _testAttemptRepository;

        public StartAttemptCommandHandler(IMapper mapper, IAsyncRepository<TestAttempt> testAttemptRepository)
        {
            _mapper = mapper;
            _testAttemptRepository = testAttemptRepository;
        }

        public async Task<Guid> Handle(StartAttemptCommand request, CancellationToken cancellationToken)
        {
            var testAttempt = _mapper.Map<TestAttempt>(request);

            testAttempt.StartTime = DateTime.UtcNow;

            testAttempt = await _testAttemptRepository.AddAsync(testAttempt);

            return testAttempt.Id;
        }
    }
}
