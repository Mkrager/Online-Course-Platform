using AutoMapper;
using Moq;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Features.Payments.Commands.UpdatePayment;
using OnlineCoursePlatform.Application.Profiles;
using OnlineCoursePlatform.Application.UnitTests.Base;
using OnlineCoursePlatform.Application.UnitTests.Mocks;
using OnlineCoursePlatform.Domain.Entities;
using Shouldly;

namespace OnlineCoursePlatform.Application.UnitTests.Payments.Commands
{
    public class UpdatePaymentCommandTests : TestBase
    {
        private readonly Mock<IAsyncRepository<Payment>> _mockPaymentRepository;

        public UpdatePaymentCommandTests()
        {
            _mockPaymentRepository = PaymentRepositoryMock.GetPaymentRepository();
        }

        [Fact]
        public async Task Should_Update_Payment_Successfully()
        {
            var handler = new UpdatePaymentCommandHandler(_mapper, _mockPaymentRepository.Object);

            var command = new UpdatePaymentCommand
            {
                Id = Guid.Parse("4f50d45e-f395-4688-a55f-c64e06649572"),
                PayerId = "sdadwd",
            };

            await handler.Handle(command, CancellationToken.None);

            var allPayments = await _mockPaymentRepository.Object.ListAllAsync();

            var createdPayment = allPayments.FirstOrDefault(a => a.PayerId == command.PayerId && a.Id == command.Id);
            createdPayment.ShouldNotBeNull();
            createdPayment.PayerId.ShouldBe(command.PayerId);
        }

    }
}
