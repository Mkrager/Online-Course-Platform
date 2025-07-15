using MediatR;
using OnlineCoursePlatform.Application.Contracts.Infrastructure;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Exceptions;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Features.PayPal.Commands.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, string>
    {
        private readonly IPayPalService _payPalService;
        private readonly IAsyncRepository<Course> _courseRepository;

        public CreateOrderCommandHandler(IPayPalService payPalService, IAsyncRepository<Course> courseRepository)
        {
            _payPalService = payPalService;
            _courseRepository = courseRepository;
        }
        public async Task<string> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var course = await _courseRepository.GetByIdAsync(request.CourseId);

            if (course == null)
                throw new NotFoundException(nameof(Course), request.CourseId);

            return await _payPalService.CreateOrderAsync(course.Price, request.ReturnUrl, request.CancelUrl);
        }
    }
}