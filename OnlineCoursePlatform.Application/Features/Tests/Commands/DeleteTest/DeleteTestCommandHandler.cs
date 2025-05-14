using AutoMapper;
using MediatR;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Exceptions;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Features.Tests.Commands.DeleteTest
{
    public class DeleteTestCommandHandler : IRequestHandler<DeleteTestCommand>
    {
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<Test> _testRepository;

        public DeleteTestCommandHandler(IMapper mapper, IAsyncRepository<Test> testRepository)
        {
            _mapper = mapper;
            _testRepository = testRepository;
        }
        public async Task<Unit> Handle(DeleteTestCommand request, CancellationToken cancellationToken)
        {
            var testToDelete = await _testRepository.GetByIdAsync(request.Id);

            if (testToDelete == null)
                throw new NotFoundException(nameof(Test), request.Id);

            await _testRepository.DeleteAsync(testToDelete);

            return Unit.Value;
        }
    }
}
