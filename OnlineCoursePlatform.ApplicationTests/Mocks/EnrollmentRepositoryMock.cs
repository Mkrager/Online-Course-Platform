using Moq;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.UnitTests.Mocks
{
    public class EnrollmentRepositoryMock
    {
        public static Mock<IEnrollmentRepository> GetEnrollmentRepository()
        {
            var enrollments = new List<Enrollment>()
            {
                new Enrollment()
                {
                    Id = Guid.Parse("ea836e17-a3be-44ef-8d38-870633446b26"),
                    CourseId = Guid.Parse("b8c3f27a-7b28-4ae6-94c2-91fdc33b77e8"),
                    StudentId = "someUserId"
                }
            };

            var mockRepository = new Mock<IEnrollmentRepository>();

            mockRepository.Setup(repo => repo.ListAllAsync())
                .ReturnsAsync(enrollments);

            mockRepository.Setup(repo => repo.AddAsync(It.IsAny<Enrollment>()))
                .ReturnsAsync((Enrollment enrollment) =>
                {
                    enrollments.Add(enrollment);
                    return enrollment;
                });

            mockRepository.Setup(repo => repo.IsUserEnrolledInCourseAsync(It.IsAny<string>(), It.IsAny<Guid>()))
                .ReturnsAsync((string userId, Guid courseId) => enrollments.Any(c => c.StudentId == userId && c.CourseId == courseId));

            return mockRepository;
        }
    }
}