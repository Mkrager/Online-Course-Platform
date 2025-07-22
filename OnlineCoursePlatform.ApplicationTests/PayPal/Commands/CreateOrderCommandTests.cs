using MediatR;
using Moq;
using OnlineCoursePlatform.Application.Contracts.Infrastructure;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Features.PayPal.Commands.CreateOrder;

namespace OnlineCoursePlatform.Application.UnitTests.PayPal.Commands
{
    public class CreateOrderCommandTests
    {
        private readonly Mock<IPayPalService> _mockPayPalService;
        private readonly Mock<ICourseRepository> _mockCourseRepository;
        private readonly Mock<IEnrollmentRepository> _mockEnrollmentRepository;
        public CreateOrderCommandTests()
        {
            _mockPayPalService = Mocks.RepositoryMocks.GetPayPalService();
            _mockCourseRepository = Mocks.RepositoryMocks.GetCourseRepository();
            _mockEnrollmentRepository = Mocks.RepositoryMocks.GetEnrollmentRepository();
        }

        [Fact]
        public async Task Handle_ReturnsValidRedirectUrl()
        {
            var handler = new CreateOrderCommandHandler(
                _mockPayPalService.Object, 
                _mockCourseRepository.Object);

            var createOrderCommand = new CreateOrderCommand()
            {
                CancelUrl = "cancel-url",
                ReturnUrl = "return-url",
                CourseId = Guid.Parse("b8c3f27a-7b28-4ae6-94c2-91fdc33b77e8"),
                UserId = "someUserId"
            };

            var result = await handler.Handle(createOrderCommand, CancellationToken.None);

            Assert.NotNull(result);
            Assert.IsType<CreateOrderResponse>(result);
        }

        [Fact]
        public async void Validator_ShouldHaveError_WhenCourseIdEmpty()
        {
            var validator = new CreateOrderCommandValidator(_mockEnrollmentRepository.Object);
            var query = new CreateOrderCommand
            {
                CancelUrl = "cancel-url",
                ReturnUrl = "return-url",
                CourseId = Guid.Empty,
                UserId = "someUserId"
            };

            var result = await validator.ValidateAsync(query);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, f => f.PropertyName == "CourseId");
        }

        [Fact]
        public async void Validator_ShouldHaveError_WhenUserAlreadyEnrolledInCourse()
        {
            var validator = new CreateOrderCommandValidator(_mockEnrollmentRepository.Object);
            var query = new CreateOrderCommand
            {
                CancelUrl = "cancel-url",
                ReturnUrl = "return-url",
                CourseId = Guid.Parse("b8c3f27a-7b28-4ae6-94c2-91fdc33b77e8"),
                UserId = "someUserId"
            };

            var result = await validator.ValidateAsync(query);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, f => string.IsNullOrEmpty(f.PropertyName) && f.ErrorMessage == "You already bought this course");
        }
    }
}
