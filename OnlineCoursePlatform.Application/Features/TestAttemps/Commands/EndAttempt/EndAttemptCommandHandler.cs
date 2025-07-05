using AutoMapper;
using MediatR;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Exceptions;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Features.TestAttemps.Commands.EndAttempt
{
    public class EndAttemptCommandHandler : IRequestHandler<EndAttemptCommand>
    {
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<TestAttempt> _testAttemptRepository;

        public EndAttemptCommandHandler(IMapper mapper, IAsyncRepository<TestAttempt> testAttemptRepository)
        {
            _mapper = mapper;
            _testAttemptRepository = testAttemptRepository;
        }

        public async Task<Unit> Handle(EndAttemptCommand request, CancellationToken cancellationToken)
        {

            var testAttemptToUpdate = await _testAttemptRepository.GetByIdAsync(request.AttempId);

            if (testAttemptToUpdate == null)
                throw new NotFoundException(nameof(TestAttempt), request.AttempId);

            // Треба доробити тут шляпу, щоб визначалася чи правильна відповідь чи щось типу того і
            // ще щось там я забув і взагалі мені все одно =)
            testAttemptToUpdate.EndTime = DateTime.UtcNow;
            testAttemptToUpdate.UserAnswers = _mapper.Map<List<UserAnswer>>(request.UserAnswerDto);

            await _testAttemptRepository.UpdateAsync(testAttemptToUpdate);

            return Unit.Value;
        }
    }
}
