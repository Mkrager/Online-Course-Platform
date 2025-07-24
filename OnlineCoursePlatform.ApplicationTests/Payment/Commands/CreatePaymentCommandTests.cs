using AutoMapper;
using Moq;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Features.Payments.Commands.CreatePayment;
using OnlineCoursePlatform.Application.Profiles;
using Shouldly;

namespace OnlineCoursePlatform.Application.UnitTests.Payments.Commands
{
    public class CreatePaymentCommandTests
    {
        private readonly Mock<IAsyncRepository<OnlineCoursePlatform.Domain.Entities.Payment>> _mockPaymentRepository;
        private readonly IMapper _mapper;

        public CreatePaymentCommandTests()
        {
            _mockPaymentRepository = Mocks.RepositoryMocks.GetPaymentRepository();
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
            };

            await handler.Handle(command, CancellationToken.None);

            var allPayments = await _mockPaymentRepository.Object.ListAllAsync();
            allPayments.Count.ShouldBe(2);

            var createdPayment = allPayments.FirstOrDefault(a => a.PayPalOrderId == command.PayPalOrderId);
            createdPayment.ShouldNotBeNull();
            createdPayment.PayPalOrderId.ShouldBe(command.PayPalOrderId);
        }
    }
}
