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
        private readonly IUserAnswerRepository _userAnswerRepository;

        public EndAttemptCommandHandler(IMapper mapper, IAsyncRepository<TestAttempt> testAttemptRepository, IUserAnswerRepository userAnswerRepository)
        {
            _mapper = mapper;
            _testAttemptRepository = testAttemptRepository;
            _userAnswerRepository = userAnswerRepository;
        }

        public async Task<Unit> Handle(EndAttemptCommand request, CancellationToken cancellationToken)
        {
            var testAttemptToUpdate = await _testAttemptRepository.GetByIdAsync(request.AttempId);

            if (testAttemptToUpdate == null)
                throw new NotFoundException(nameof(TestAttempt), request.AttempId);

            testAttemptToUpdate.EndTime = DateTime.UtcNow;
            testAttemptToUpdate.UserAnswers = _mapper.Map<List<UserAnswer>>(request.UserAnswerDto);

            var result = await _userAnswerRepository.PopulateIsCorrectAsync(_mapper.Map<List<UserAnswer>>(testAttemptToUpdate.UserAnswers));

            await _testAttemptRepository.UpdateAsync(testAttemptToUpdate);

            return Unit.Value;
        }
    }
}
