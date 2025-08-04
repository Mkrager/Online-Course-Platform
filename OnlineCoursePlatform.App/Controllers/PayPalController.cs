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
    }
}
