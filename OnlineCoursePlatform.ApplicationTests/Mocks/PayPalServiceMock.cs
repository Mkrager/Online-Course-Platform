using Moq;
using OnlineCoursePlatform.Application.Contracts.Infrastructure;
using OnlineCoursePlatform.Application.DTOs.PayPal;

namespace OnlineCoursePlatform.Application.UnitTests.Mocks
{
    public class PayPalServiceMock
    {
        public static Mock<IPayPalService> GetPayPalService()
        {
            var mockService = new Mock<IPayPalService>();

            mockService.Setup(service => service.CreateOrderAsync(It.IsAny<decimal>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new CreateOrderResponse()
                {
                    OrderId = "orderId",
                    RedirectUrl = "redirectUrl"
                });

            return mockService;
        }
    }
}
