using AutoMapper;
using MediatR;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Features.Questions.Queries.GetQuestionList
{
    public class GetAnswerListQueryHandler : IRequestHandler<GetAnswerListQuery, List<QuestionListlVm>>
    {
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<Question> _questionRepository;

        public GetAnswerListQueryHandler(IMapper mapper, IAsyncRepository<Question> questionRepository)
        {
            _mapper = mapper;
            _questionRepository = questionRepository;
        }

        public async Task<List<QuestionListlVm>> Handle(GetAnswerListQuery request, CancellationToken cancellationToken)
        {
            var allQuestions = (await _questionRepository.ListAllAsync()).OrderBy(o => o.Id);

            return _mapper.Map<List<QuestionListlVm>>(allQuestions);
        }
    }
}
