using Moq;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Features.Courses.Commands.UpdateCourse;
using OnlineCoursePlatform.Application.Features.SupportTickets.Commands.UpdateSupportTicket.InProgressTicket;
using OnlineCoursePlatform.Application.UnitTests.Base;
using OnlineCoursePlatform.Application.UnitTests.Mocks;
using OnlineCoursePlatform.Domain.Entities;
using OnlineCoursePlatform.Domain.Enums;
using Shouldly;

namespace OnlineCoursePlatform.Application.UnitTests.SupportTickets.Commands
{
    public class InProgressTicketCommandTest : TestBase
    {
        private readonly Mock<IAsyncRepository<SupportTicket>> _mockSupportTicketRepository;

        public InProgressTicketCommandTest()
        {
            _mockSupportTicketRepository = SupportTicketRepositoryMock.GetCourseRepository();
        }

        [Fact]
        public async Task UpdateSupportTicket_ValidCommand_UpdatesSupportTicketSuccessfully()
        {
            var handler = new InProgressTicketCommandHandler(_mockSupportTicketRepository.Object, _mapper);
            var updateCommand = new InProgressTicketCommand
            {
                Id = Guid.Parse("b8c3f27a-7b28-4ae6-94c2-91fdc33b77e8"),
            };

            await handler.Handle(updateCommand, CancellationToken.None);

            var updatedSupportTicket = await _mockSupportTicketRepository.Object.GetByIdAsync(updateCommand.Id);

            updatedSupportTicket.ShouldNotBeNull();
            updatedSupportTicket.Status.ShouldBe(SupportStatus.InProgress);
        }
    }
}