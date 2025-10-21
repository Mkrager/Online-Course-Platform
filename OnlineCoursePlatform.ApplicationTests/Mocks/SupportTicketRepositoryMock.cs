using Moq;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.UnitTests.Mocks
{
    public class SupportTicketRepositoryMock
    {
        public static Mock<IAsyncRepository<SupportTicket>> GetCourseRepository()
        {
            var supportTickets = new List<SupportTicket>
            {
                new SupportTicket
                {
                    Id = Guid.Parse("b8c3f27a-7b28-4ae6-94c2-91fdc33b77e8"),
                    Message = "test2",
                    Subject = "test2",
                },
                new SupportTicket
                {
                    Id = Guid.Parse("b8c3f27a-7b28-4ae6-94c2-91fdc33b77e2"),
                    Message = "test",
                    Subject = "test",
                },
            };

            var mockRepository = new Mock<IAsyncRepository<SupportTicket>>();

            mockRepository.Setup(r => r.ListAllAsync())
                .ReturnsAsync(supportTickets);

            mockRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid id) => supportTickets.FirstOrDefault(x => x.Id == id));

            mockRepository.Setup(r => r.AddAsync(It.IsAny<SupportTicket>()))
                .ReturnsAsync((SupportTicket supportTicket) =>
                {
                    supportTickets.Add(supportTicket);
                    return supportTicket;
                });

            mockRepository.Setup(r => r.UpdateAsync(It.IsAny<SupportTicket>()))
                .Callback((SupportTicket supportTicket) =>
                {
                    var oldsupportTicket = supportTickets.FirstOrDefault(x => x.Id == supportTicket.Id);
                    if (oldsupportTicket != null)
                    {
                        oldsupportTicket.Message = supportTicket.Message;
                        oldsupportTicket.Subject = supportTicket.Subject;
                        oldsupportTicket.Status = supportTicket.Status;
                    }
                });

            mockRepository.Setup(r => r.DeleteAsync(It.IsAny<SupportTicket>()))
                .Callback((SupportTicket supportTicket) => supportTickets.Remove(supportTicket));

            return mockRepository;
        }
    }
}