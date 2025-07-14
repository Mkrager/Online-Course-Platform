using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineCoursePlatform.Application.Features.PayPal.Commands.CreateOrder;

namespace OnlineCoursePlatform.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PayPalController(IMediator mediator, IConfiguration configuration) : Controller
    {
        [HttpPost(Name = "CreateOrder")]
        public async Task<IActionResult> CreateOrder(Guid courseId)
        {
            var baseUrl = configuration["App:BaseUrl"];
            var returnUrl = $"{baseUrl}/payment/success?courseId={courseId}";
            var cancelUrl = $"{baseUrl}/payment/cancel";

            var result = await mediator.Send(new CreateOrderCommand()
            {
                CourseId = courseId,
                CancelUrl = cancelUrl,
                ReturnUrl = returnUrl
            });

            return Ok(new { url = result });
        }
    }
}
