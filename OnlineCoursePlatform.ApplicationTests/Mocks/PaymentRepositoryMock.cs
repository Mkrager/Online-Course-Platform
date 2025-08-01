using Moq;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.UnitTests.Mocks
{
    public class PaymentRepositoryMock
    {
        public static Mock<IAsyncRepository<Payment>> GetPaymentRepository()
        {
            var payments = new List<Payment>()
            {
                new Payment()
                {
                    Id = Guid.Parse("4f50d45e-f395-4688-a55f-c64e06649572")
                }
            };

            var mockRepository = new Mock<IAsyncRepository<Payment>>();

            mockRepository.Setup(repo => repo.ListAllAsync())
                .ReturnsAsync(payments);

            mockRepository.Setup(repo => repo.AddAsync(It.IsAny<Payment>()))
                .ReturnsAsync((Payment payment) =>
                {
                    payments.Add(payment);
                    return payment;
                });

            mockRepository.Setup(r => r.UpdateAsync(It.IsAny<Payment>()))
                .Callback((Payment course) =>
                {
                    var oldCourse = payments.FirstOrDefault(x => x.Id == course.Id);
                    if (oldCourse != null)
                    {
                        oldCourse.CourseId = course.CourseId;
                        oldCourse.Status = course.Status;
                        oldCourse.PayerId = course.PayerId;
                        oldCourse.PayPalOrderId = course.PayPalOrderId;
                    }
                });

            mockRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid id) => payments.FirstOrDefault(x => x.Id == id));

            mockRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid id) => payments.FirstOrDefault(x => x.Id == id));


            return mockRepository;
        }

    }
}
