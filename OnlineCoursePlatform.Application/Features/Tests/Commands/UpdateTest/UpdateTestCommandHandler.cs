using AutoMapper;
using MediatR;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Exceptions;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Features.Tests.Commands.UpdateTest
{
    public class UpdateTestCommandHandler : IRequestHandler<UpdateTestCommand>
    {
        private readonly IMapper _mapper;
        private readonly ITestRepository _testRepository;
        public UpdateTestCommandHandler(IMapper mapper, ITestRepository testRepository)
        {
            _mapper = mapper;
            _testRepository = testRepository;
        }

        public async Task<Unit> Handle(UpdateTestCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateTestCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
                throw new ValidationException(validationResult);

            var testToUpdate = await _testRepository.GetTestWithQuestionsAndAnswers(request.Id);

            if (testToUpdate == null)
                throw new NotFoundException(nameof(Test), request.Id);

            _mapper.Map(request, testToUpdate, typeof(UpdateTestCommand), typeof(Test));

            await _testRepository.UpdateAsync(testToUpdate);

            return Unit.Value;
        }
    }
}
