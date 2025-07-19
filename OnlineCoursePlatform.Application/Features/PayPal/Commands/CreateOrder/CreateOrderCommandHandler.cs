using MediatR;
using OnlineCoursePlatform.Application.Contracts.Infrastructure;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Exceptions;
using OnlineCoursePlatform.Application.Features.Payments.Commands.CreatePayment;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Features.PayPal.Commands.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, string>
    {
        private readonly IPayPalService _payPalService;
        private readonly IAsyncRepository<Course> _courseRepository;
        private readonly IMediator _mediator;

        public CreateOrderCommandHandler(
            IPayPalService payPalService, 
            IAsyncRepository<Course> courseRepository,
            IMediator mediator)
        {
            _payPalService = payPalService;
            _courseRepository = courseRepository;
            _mediator = mediator;
        }
        public async Task<string> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var course = await _courseRepository.GetByIdAsync(request.CourseId);

            if (course == null)
                throw new NotFoundException(nameof(Course), request.CourseId);

            var result = await _payPalService.CreateOrderAsync(course.Price, request.ReturnUrl, request.CancelUrl);

            await _mediator.Send(new CreatePaymentCommand()
            {
                PayPalOrderId = result.OrderId,
                UserId = request.UserId,
                CourseId = request.CourseId
            });

            return result.RedirectUrl;
        }
    }
}