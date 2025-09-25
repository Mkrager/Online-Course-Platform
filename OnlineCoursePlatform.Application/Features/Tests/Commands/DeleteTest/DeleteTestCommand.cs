using MediatR;
using OnlineCoursePlatform.Application.Common.Interfaces;

namespace OnlineCoursePlatform.Application.Features.Tests.Commands.DeleteTest
{
    public class DeleteTestCommand : IRequest, IUserRequest
    {
        public Guid Id { get; set; }

        public string UserId { get; set; } = string.Empty;
        public string UserRoles { get; set; } = string.Empty;
    }
}