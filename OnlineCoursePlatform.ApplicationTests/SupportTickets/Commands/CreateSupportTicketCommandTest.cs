using Moq;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Features.SupportTickets.Commands.CreateSupportTicket;
using OnlineCoursePlatform.Application.UnitTests.Base;
using OnlineCoursePlatform.Application.UnitTests.Mocks;
using OnlineCoursePlatform.Domain.Entities;
using OnlineCoursePlatform.Domain.Enums;
using Shouldly;

namespace OnlineCoursePlatform.Application.UnitTests.SupportTickets.Commands
{
    public class CreateSupportTicketCommandTest : TestBase
    {
        private readonly Mock<IAsyncRepository<SupportTicket>> _mockSupportTicketRepository;

        public CreateSupportTicketCommandTest()
        {
            _mockSupportTicketRepository = SupportTicketRepositoryMock.GetCourseRepository();
        }

        [Fact]
        public async Task Should_Create_SupportTicket_Successfully()
        {
            var handler = new CreateSupportTicketCommandHandler(_mockSupportTicketRepository.Object, _mapper);

            var command = new CreateSupportTicketCommand
            {
                Subject = "testSubject123",
                Message = "testMessage123",
            };

            await handler.Handle(command, CancellationToken.None);

            var allSupportTickets = await _mockSupportTicketRepository.Object.ListAllAsync();
            allSupportTickets.Count.ShouldBe(3);

            var createdSupportTicket = allSupportTickets.FirstOrDefault(a => a.Subject == command.Subject && a.Message == command.Message);
            createdSupportTicket.ShouldNotBeNull();
            createdSupportTicket.Subject.ShouldBe(command.Subject);
            createdSupportTicket.Message.ShouldBe(command.Message);
            createdSupportTicket.Status.ShouldBe(SupportStatus.Open);
        }

        [Fact]
        public async void Validator_ShouldHaveError_WhenSubjectEmpty()
        {
            var validator = new CreateSupportTicketCommandValidator();
            var query = new CreateSupportTicketCommand
            {
                Subject = "",
                Message = "lalalalala"
            };

            var result = await validator.ValidateAsync(query);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, f => f.PropertyName == "Subject");
        }
    }
}