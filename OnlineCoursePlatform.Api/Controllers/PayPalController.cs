using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineCoursePlatform.Application.Contracts;
using OnlineCoursePlatform.Application.Features.Enrollments.Commands.CreateEnrollment;
using OnlineCoursePlatform.Application.Features.Payments.Commands.CreatePayment;
using OnlineCoursePlatform.Application.Features.Payments.Commands.UpdatePayment;
using OnlineCoursePlatform.Application.Features.Payments.Queries.GetPaymentDetail;
using OnlineCoursePlatform.Application.Features.PayPal.Commands.CaptureOrder;
using OnlineCoursePlatform.Application.Features.PayPal.Commands.CreateOrder;
using OnlineCoursePlatform.Domain.Enums;

namespace OnlineCoursePlatform.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PayPalController(IMediator mediator, IConfiguration configuration, ICurrentUserService currentUserService) : Controller
    {
        [HttpPost("create-order", Name = "CreateOrder")]
        public async Task<IActionResult> CreateOrder(Guid courseId)
        {
            //var userId = currentUserService.UserId;

            var paymentId = await mediator.Send(new CreatePaymentCommand()
            {
                //UserId = userId,
                CourseId = courseId
            });

            var baseUrl = configuration["App:BaseUrl"];
            var returnUrl = $"{baseUrl}/api/paypal/capture-order?paymentId={paymentId}";
            var cancelUrl = $"{baseUrl}/api/payment/cancel";

            var result = await mediator.Send(new CreateOrderCommand()
            {
                CancelUrl = cancelUrl,
                ReturnUrl = returnUrl,
                CourseId = courseId,
                //UserId = userId
            });

            await mediator.Send(new UpdatePaymentCommand()
            {
                Id = paymentId,
                PayPalOrderId = result.PayPalOrderId,
                Status = OrderStatus.Pending
            });

            return Ok(new { url = result.RedirectUrl });
        }

        [HttpGet("capture-order", Name = "CaptureOrder")]
        public async Task<IActionResult> CaptureOrder(
            [FromQuery] Guid paymentId, 
            [FromQuery] string token, 
            [FromQuery] string payerId)
        {
            var payment = await mediator.Send(new GetPaymentDetailQuery()
            {
                Id = paymentId
            });

            var result = await mediator.Send(new CaptureOrderCommand()
            {
                OrderId = token,
                PayerId = payerId
            });

            await mediator.Send(new UpdatePaymentCommand()
            {
                Id = paymentId,
                PayerId = payerId,
                Status = OrderStatus.Completed,
                PayPalOrderId = payment.PayPalOrderId
            });

            await mediator.Send(new CreateEnrollmentCommand()
            {
                CourseId = payment.CourseId,
                StudentId = payment.UserId
            });

            return Ok(result);
        }
    }
}
