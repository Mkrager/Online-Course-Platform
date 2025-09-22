using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineCoursePlatform.Application.Contracts;
using OnlineCoursePlatform.Application.Contracts.Application;
using OnlineCoursePlatform.Application.DTOs.PayPal;
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
        [Authorize(Roles = "Default")]
        [HttpPost("create-order", Name = "CreateOrder")]
        public async Task<IActionResult> CreateOrder(Guid courseId)
        {
            var userId = currentUserService.UserId;

            var redirectUrl = await checkoutService.CreateOrderAsync(courseId, userId);

            return Ok(new { url = redirectUrl });
        }

        [Authorize(Roles = "Default")]
        [HttpPost("capture-order", Name = "CaptureOrder")]
        public async Task<IActionResult> CaptureOrder(CaptureOrderRequest captureOrderRequest)
        {
            var result = await checkoutService.CaptureOrderAsync(captureOrderRequest);
            return Ok(result);
        }

        [Authorize(Roles = "Default")]
        [HttpPatch("cancel", Name = "CancelOrder")]
        public async Task<IActionResult> Cancel(UpdatePaymentCommand updatePaymentCommand)
        {
            updatePaymentCommand.Status = OrderStatus.Canceled;

            var result = await mediator.Send(updatePaymentCommand);

            return NoContent();
        }
    }
}
