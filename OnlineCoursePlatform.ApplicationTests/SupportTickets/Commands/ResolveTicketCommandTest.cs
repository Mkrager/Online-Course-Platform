using Moq;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Features.SupportTickets.Commands.UpdateSupportTicket.CloseTicket;
using OnlineCoursePlatform.Application.Features.SupportTickets.Commands.UpdateSupportTicket.ResolveTicket;
using OnlineCoursePlatform.Application.UnitTests.Base;
using OnlineCoursePlatform.Application.UnitTests.Mocks;
using OnlineCoursePlatform.Domain.Entities;
using OnlineCoursePlatform.Domain.Enums;
using Shouldly;

namespace OnlineCoursePlatform.Application.UnitTests.SupportTickets.Commands
{
    public class ResolveTicketCommandTest : TestBase
    {
        private readonly Mock<IAsyncRepository<SupportTicket>> _mockSupportTicketRepository;

        public ResolveTicketCommandTest()
        {
            _mockSupportTicketRepository = SupportTicketRepositoryMock.GetCourseRepository();
        }

        [Fact]
        public async Task UpdateSupportTicket_ValidCommand_UpdatesSupportTicketSuccessfully()
        {
            var handler = new ResolveTicketCommandHandler(_mockSupportTicketRepository.Object, _mapper);
            var updateCommand = new ResolveTicketCommand
            {
                Id = Guid.Parse("b8c3f27a-7b28-4ae6-94c2-91fdc33b77e8"),
            };

            await handler.Handle(updateCommand, CancellationToken.None);

            var updatedSupportTicket = await _mockSupportTicketRepository.Object.GetByIdAsync(updateCommand.Id);

            updatedSupportTicket.ShouldNotBeNull();
            updatedSupportTicket.Status.ShouldBe(SupportStatus.Resolved);
        }
    }
}