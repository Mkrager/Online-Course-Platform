using MediatR;

namespace OnlineCoursePlatform.Application.Features.Tests.Queries.GetTestDetail
{
    public class GetTestDetailQuery : IRequest<TestDetailVm>
    {
        public Guid Id { get; set; }
    }
}
