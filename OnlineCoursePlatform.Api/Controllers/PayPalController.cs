using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineCoursePlatform.Application.Contracts;
using OnlineCoursePlatform.Application.Features.PayPal.Commands.CaptureOrder;
using OnlineCoursePlatform.Application.Features.PayPal.Commands.CreateOrder;

namespace OnlineCoursePlatform.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PayPalController(IMediator mediator, IConfiguration configuration, ICurrentUserService currentUserService) : Controller
    {
        [HttpPost("create-order", Name = "CreateOrder")]
        public async Task<IActionResult> CreateOrder(Guid courseId)
        {
            var baseUrl = configuration["App:BaseUrl"];
            var returnUrl = $"{baseUrl}/api/paypal/capture-order";
            var cancelUrl = $"{baseUrl}/api/payment/cancel";

            var result = await mediator.Send(new CreateOrderCommand()
            {
                CancelUrl = cancelUrl,
                ReturnUrl = returnUrl,
                CourseId = courseId,
                UserId = currentUserService.UserId
            });

            return Ok(new { url = result });
        }

        [HttpGet("capture-order", Name = "CaptureOrder")]
        public async Task<IActionResult> CaptureOrder([FromQuery] string token, [FromQuery] string payerId)
        {
            var result = await mediator.Send(new CaptureOrderCommand()
            {
                OrderId = token,
                PayerId = payerId
            });

            return Ok(result);
        }
    }
}
