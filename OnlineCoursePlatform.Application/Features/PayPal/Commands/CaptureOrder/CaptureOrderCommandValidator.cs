using FluentValidation;

namespace OnlineCoursePlatform.Application.Features.PayPal.Commands.CaptureOrder
{
    public class CaptureOrderCommandValidator : AbstractValidator<CaptureOrderCommand>
    {
        public CaptureOrderCommandValidator()
        {
            RuleFor(r => r.OrderId)
                .NotEmpty().WithMessage("Empty orderId");
        }
    }
}
