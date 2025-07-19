using AutoMapper;
using Moq;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Features.Enrollments.Commands.CreateEnrollment;
using OnlineCoursePlatform.Application.Features.Payments.Commands.CreatePayment;
using OnlineCoursePlatform.Application.Profiles;
using OnlineCoursePlatform.Domain.Entities;
using Shouldly;

namespace OnlineCoursePlatform.Application.UnitTests.Payments.Commands
{
    public class CreatePaymentCommandTests
    {
        private readonly Mock<IAsyncRepository<Payment>> _mockPaymentRepository;
        private readonly IMapper _mapper;

        public CreatePaymentCommandTests()
        {
            _mockPaymentRepository = Mocks.RepositoryMocks.GetPaymantRepository();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task Should_Create_Payment_Successfully()
        {
            var handler = new CreatePaymentCommandHandler(_mapper, _mockPaymentRepository.Object);

            var command = new CreatePaymentCommand
            {
                PayPalOrderId = "orderId",
                PayerId = "payerId",
                UserId = "userId"
            };

            await handler.Handle(command, CancellationToken.None);

            var allPayments = await _mockPaymentRepository.Object.ListAllAsync();
            allPayments.Count.ShouldBe(2);

            var createdPayment = allPayments.FirstOrDefault(a => a.PayPalOrderId == command.PayPalOrderId && a.PayerId == command.PayerId);
            createdPayment.ShouldNotBeNull();
            createdPayment.PayPalOrderId.ShouldBe(command.PayPalOrderId);
            createdPayment.PayerId.ShouldBe(command.PayerId);
        }
    }
}
