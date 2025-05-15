using AutoMapper;
using MediatR;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Exceptions;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Features.Tests.Commands.CreateTest
{
    public class CreateTestCommandHandler : IRequestHandler<CreateTestCommand, Guid>
    {
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<Test> _testRepository;
        public CreateTestCommandHandler(IMapper mapper, IAsyncRepository<Test> testRepository)
        {
            _mapper = mapper;
            _testRepository = testRepository;
        }

        public async Task<Guid> Handle(CreateTestCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateTestCommandValidator();
            var validatorResult = await validator.ValidateAsync(request);

            if (validatorResult.Errors.Count > 0)
                throw new ValidationException(validatorResult);

            var @test = _mapper.Map<Test>(request);            

            @test = await _testRepository.AddAsync(test);

            return @test.Id;
        }
    }
}
