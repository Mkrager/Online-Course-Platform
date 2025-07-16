using Moq;
using OnlineCoursePlatform.Application.Contracts.Infrastructure;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Features.PayPal.Commands.CreateOrder;

namespace OnlineCoursePlatform.Application.UnitTests.PayPal.Commands
{
    public class CreateOrderCommandTests
    {
        private readonly Mock<IPayPalService> _mockPayPalService;
        private readonly Mock<ICourseRepository> _mockCourseRepository;
        public CreateOrderCommandTests()
        {
            _mockPayPalService = Mocks.RepositoryMocks.GetPayPalService();
            _mockCourseRepository = Mocks.RepositoryMocks.GetCourseRepository();
        }

        [Fact]
        public async Task Handle_ReturnsValidRedirectUrl()
        {
            var handler = new CreateOrderCommandHandler(_mockPayPalService.Object, _mockCourseRepository.Object);

            var createOrderCommand = new CreateOrderCommand()
            {
                CancelUrl = "cancel-url",
                ReturnUrl = "return-url",
                CourseId = Guid.Parse("b8c3f27a-7b28-4ae6-94c2-91fdc33b77e8")
            };

            var result = await handler.Handle(createOrderCommand, CancellationToken.None);

            Assert.NotNull(result);
            Assert.IsType<string>(result);
        }
    }
}
