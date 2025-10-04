using Moq;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Domain.Entities;
using OnlineCoursePlatform.Domain.Enums;

namespace OnlineCoursePlatform.Application.UnitTests.Mocks
{
    public class TeacherApplicationRepositoryMock
    {
        public static Mock<IRequestRepository<TeacherApplication>> GetTeacherApplicationRepository()
        {
            var teacherApplications = new List<TeacherApplication>()
            {
                new TeacherApplication()
                {
                    Id = Guid.Parse("6ba684fb-e3bc-418c-8971-2f302b43daf6"),
                    Status = RequestStatus.Pending,
                    CreatedBy = "userId"
                },
                new TeacherApplication()
                {
                    Id = Guid.Parse("e706a9cf-6e56-46ed-896a-eadcad69c90f"),
                    CreatedBy = "userId"
                }
            };

            var mockRepository = new Mock<IRequestRepository<TeacherApplication>>();

            mockRepository.Setup(repo => repo.ListAllAsync())
                .ReturnsAsync(teacherApplications);


            mockRepository.Setup(r => r.AddAsync(It.IsAny<TeacherApplication>()))
                .ReturnsAsync((TeacherApplication teacherApplication) =>
                {
                    teacherApplications.Add(teacherApplication);
                    return teacherApplication;
                });

            mockRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid id) => teacherApplications.FirstOrDefault(x => x.Id == id));

            mockRepository.Setup(r => r.GetRequestByUserIdAsync(It.IsAny<string>()))
                .ReturnsAsync((string userId) => teacherApplications.Where(r => r.CreatedBy == userId).ToList());

            mockRepository.Setup(repo => repo.GetRequestsByStatusAsync(It.IsAny<RequestStatus?>()))
                .ReturnsAsync((RequestStatus? status) =>
                {
                    if (status.HasValue)
                        return teacherApplications.Where(r => r.Status == status).ToList();

                    return teacherApplications;
                });

            mockRepository.Setup(r => r.UpdateStatusAsync(It.IsAny<TeacherApplication>(), It.IsAny<RequestStatus>(), It.IsAny<string?>()))
                .Callback((TeacherApplication teacherApplication, RequestStatus newStatus, string? reason) =>
                {
                    var oldTeacherApplication = teacherApplications.FirstOrDefault(x => x.Id == teacherApplication.Id);
                    if (oldTeacherApplication != null)
                    {
                        oldTeacherApplication.Status = newStatus;
                        oldTeacherApplication.RejectReason = reason;
                    }
                });

            return mockRepository;
        }
    }
}
