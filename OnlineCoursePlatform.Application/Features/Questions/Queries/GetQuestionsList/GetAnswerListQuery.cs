using MediatR;

namespace OnlineCoursePlatform.Application.Features.Questions.Queries.GetQuestionList
{
    public class GetAnswerListQuery : IRequest<List<QuestionListlVm>>
    {
        public Guid Id { get; set; }
    }
}
