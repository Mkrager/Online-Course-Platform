using AutoMapper;
using MediatR;
using OnlineCoursePlatform.Application.Contracts.Persistance;

namespace OnlineCoursePlatform.Application.Features.Tests.Queries.GetUserTestsList
{
    public class GetLessonTestsQueryHandler : IRequestHandler<GetLessonTestsQuery, List<LessonTestListVm>>
    {
        private readonly IMapper _mapper;
        private readonly ITestRepository _testRepository;

        public GetLessonTestsQueryHandler(IMapper mapper, ITestRepository testRepository)
        {
            _mapper = mapper;
            _testRepository = testRepository;
        }

        public async Task<List<LessonTestListVm>> Handle(GetLessonTestsQuery request, CancellationToken cancellationToken)
        {
            var allLessonTests = await _testRepository.GetTestsByLessonIdAsync(request.LessonId);

            return _mapper.Map<List<LessonTestListVm>>(allLessonTests);
        }
    }
}