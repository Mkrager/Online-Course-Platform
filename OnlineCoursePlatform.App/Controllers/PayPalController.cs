using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineCoursePlatform.App.Contracts;
using OnlineCoursePlatform.App.ViewModels.PayPal;

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
        [Authorize]
        public async Task<IActionResult> CreateOrder(Guid courseId)
        {
            var url = await _payPalService.CreateOrderAsync(courseId);
            return Ok(url);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> CaptureOrder(
            [FromQuery] Guid paymentId,
            [FromQuery] string token,
            [FromQuery] string payerId)
        {
            var result = await _payPalService.CaptureOrderAsync(new CaptureOrderRequest()
            {
                PayerId = payerId,
                PaymentId = paymentId,
                Token = token
            });
            
            if(result.IsSuccess)
                return RedirectToAction("PaymentSuccess", "PayPal");

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Authorize]
        public IActionResult PaymentSuccess()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Cancel([FromQuery] Guid paymentId)
        {
            var result = await _payPalService.CancelOrderAsync(new CancelOrderViewModel
            {
                Id = paymentId
            });

            if (result.IsSuccess)
                return RedirectToAction("CancelPayment", "PayPal");

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Authorize]
        public IActionResult CancelPayment()
        {
            return View();
        }
    }
}
