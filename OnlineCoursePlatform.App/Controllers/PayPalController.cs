using Microsoft.AspNetCore.Mvc;
using OnlineCoursePlatform.App.Contracts;

namespace OnlineCoursePlatform.App.Controllers
{
    public class PayPalController : Controller
    {
        private readonly IPayPalService _payPalService;
        public PayPalController(IPayPalService payPalService)
        {
            _payPalService = payPalService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(Guid courseId)
        {
            var url = await _payPalService.CreateOrderAsync(courseId);
            return Ok(url);
        }

        [HttpGet]
        public async Task<IActionResult> CaptureOrder(
            [FromQuery] Guid paymentId,
            [FromQuery] string token,
            [FromQuery] string payerId)
        {
            var result = await _payPalService.CaptureOrderAsync(paymentId, token, payerId);
            
            if(result.IsSuccess)
                return RedirectToAction("PaymentSuccess", "PayPal");

            return View(result.ErrorText);
        }

        [HttpGet]
        public IActionResult PaymentSuccess()
        {
            return View();
        }
    }
}
