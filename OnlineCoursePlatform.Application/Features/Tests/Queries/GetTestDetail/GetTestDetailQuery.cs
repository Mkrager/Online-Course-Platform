using MediatR;
using OnlineCoursePlatform.Application.Common.Interfaces;

namespace OnlineCoursePlatform.Application.Features.Tests.Queries.GetTestDetail
{
    public class GetTestDetailQuery : IRequest<TestDetailVm>, IUserRequest
    {
        public Guid Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string UserRoles { get; set; } = string.Empty;
    }
}
