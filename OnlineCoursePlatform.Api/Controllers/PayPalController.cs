using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineCoursePlatform.Application.Contracts;
using OnlineCoursePlatform.Application.Contracts.Services;
using OnlineCoursePlatform.Application.Features.Payments.Commands.UpdatePayment;
using OnlineCoursePlatform.Domain.Enums;

namespace OnlineCoursePlatform.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PayPalController(
        ICurrentUserService currentUserService, 
        ICheckoutService checkoutService,
        IMediator mediator) : Controller
    {
        [HttpPost("create-order", Name = "CreateOrder")]
        public async Task<IActionResult> CreateOrder(Guid courseId)
        {
            var userId = currentUserService.UserId;

            var redirectUrl = await checkoutService.CreateOrderAsync(courseId, userId);

            return Ok(new { url = redirectUrl });
        }

        [HttpPost("capture-order", Name = "CaptureOrder")]
        public async Task<IActionResult> CaptureOrder(
            [FromQuery] Guid paymentId, 
            [FromQuery] string token, 
            [FromQuery] string payerId)
        {
            var result = await checkoutService.CaptureOrderAsync(paymentId, token, payerId);
            return Ok(result);
        }

        [HttpPatch("cancel", Name = "CancelOrder")]
        public async Task<IActionResult> Cancel(UpdatePaymentCommand updatePaymentCommand)
        {
            updatePaymentCommand.Status = OrderStatus.Canceled;

            var result = await mediator.Send(updatePaymentCommand);

            return NoContent();
        }
    }
}
