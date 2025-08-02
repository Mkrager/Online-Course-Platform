using Microsoft.AspNetCore.Mvc;
using OnlineCoursePlatform.Application.Contracts;
using OnlineCoursePlatform.Application.Contracts.Services;

namespace OnlineCoursePlatform.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PayPalController(ICurrentUserService _currentUserService, ICheckoutService checkoutService) : Controller
    {
        [HttpPost("create-order", Name = "CreateOrder")]
        public async Task<IActionResult> CreateOrder(Guid courseId)
        {
            var userId = _currentUserService.UserId;

            var redirectUrl = await checkoutService.CreateOrderAsync(courseId, userId);

            return Ok(new { url = redirectUrl });
        }

        [HttpGet("capture-order", Name = "CaptureOrder")]
        public async Task<IActionResult> CaptureOrder(
            [FromQuery] Guid paymentId, 
            [FromQuery] string token, 
            [FromQuery] string payerId)
        {
            var result = await checkoutService.CaptureOrderAsync(paymentId, token, payerId);
            return Ok(result);
        }
    }
}
