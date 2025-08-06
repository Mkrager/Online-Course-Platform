using MediatR;
using OnlineCoursePlatform.Application.Contracts.Infrastructure;
using OnlineCoursePlatform.Application.Contracts.Services;
using OnlineCoursePlatform.Application.DTOs.PayPal;
using OnlineCoursePlatform.Application.Features.Enrollments.Commands.CreateEnrollment;
using OnlineCoursePlatform.Application.Features.Payments.Commands.CreatePayment;
using OnlineCoursePlatform.Application.Features.Payments.Commands.UpdatePayment;
using OnlineCoursePlatform.Application.Features.Payments.Queries.GetPaymentDetail;
using OnlineCoursePlatform.Application.Features.PayPal.Commands.CaptureOrder;
using OnlineCoursePlatform.Application.Features.PayPal.Commands.CreateOrder;
using OnlineCoursePlatform.Domain.Enums;

namespace OnlineCoursePlatform.Application.Services
{
    public class CheckoutService : ICheckoutService
    {
        private readonly IMediator _mediator;
        private readonly IBaseUrlProvider _baseUrlProvider;
        public CheckoutService(IMediator mediator, IBaseUrlProvider baseUrlProvider)
        {
            _mediator = mediator;
            _baseUrlProvider = baseUrlProvider;
        }
        public async Task<string> CreateOrderAsync(Guid courseId, string userId)
        {
            var paymentId = await _mediator.Send(new CreatePaymentCommand()
            {
                CourseId = courseId,
            });

            var baseUrl = _baseUrlProvider.BaseUrl;
            var returnUrl = $"{baseUrl}/paypal/captureOrder?paymentId={paymentId}";
            var cancelUrl = $"{baseUrl}/paypal/cancel?paymentId={paymentId}";

            var result = await _mediator.Send(new CreateOrderCommand()
            {
                CancelUrl = cancelUrl,
                ReturnUrl = returnUrl,
                CourseId = courseId,
                UserId = userId
            });

            await _mediator.Send(new UpdatePaymentCommand()
            {
                Id = paymentId,
                PayPalOrderId = result.PayPalOrderId,
                Status = OrderStatus.Pending
            });

            return result.RedirectUrl;
        }
        public async Task<bool> CaptureOrderAsync(CaptureOrderRequest captureOrderRequest)
        {
            var payment = await _mediator.Send(new GetPaymentDetailQuery()
            {
                Id = captureOrderRequest.PaymentId
            });

            var result = await _mediator.Send(new CaptureOrderCommand()
            {
                OrderId = captureOrderRequest.Token,
                PayerId = captureOrderRequest.PayerId
            });

            await _mediator.Send(new UpdatePaymentCommand()
            {
                Id = captureOrderRequest.PaymentId,
                PayerId = captureOrderRequest.PayerId,
                Status = OrderStatus.Completed,
                PayPalOrderId = payment.PayPalOrderId
            });

            await _mediator.Send(new CreateEnrollmentCommand()
            {
                CourseId = payment.CourseId,
                StudentId = payment.UserId
            });

            return result;
        }
    }
}
