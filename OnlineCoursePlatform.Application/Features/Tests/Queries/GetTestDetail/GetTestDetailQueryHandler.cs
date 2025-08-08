using AutoMapper;
using MediatR;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Exceptions;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Features.Tests.Queries.GetTestDetail
{
    public class GetTestDetailQueryHandler : IRequestHandler<GetTestDetailQuery, TestDetailVm>
    {
        private readonly IMapper _mapper;
        private readonly ITestRepository _testRepository;

        public GetTestDetailQueryHandler(IMapper mapper, ITestRepository testRepository)
        {
           _mapper = mapper;
            _testRepository = testRepository;
        }

        public async Task<TestDetailVm> Handle(GetTestDetailQuery request, CancellationToken cancellationToken)
        {
            var test = await _testRepository.GetTestWithQuestionsAndAnswersAsync(request.Id);

            if (test == null)
            {
                throw new NotFoundException(nameof(Test), request.Id);
            }

            return _mapper.Map<TestDetailVm>(test);
        }
    }
}
