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
                },

                new Enrollment()
                {
                    Id = Guid.Parse("fbd4d162-5215-4773-b57a-ecfc960dc1d1"),
                    CourseId = Guid.Parse("c068db61-de7d-4cf3-bffd-b9278de471d0"),
                    StudentId = "123",
                    Course = new Course()
                    {
                        Id = Guid.Parse("c068db61-de7d-4cf3-bffd-b9278de471d0")
                    }
                },

                new Enrollment()
                {
                    Id = Guid.Parse("b3b5248a-086c-4b9f-93a5-df384ee94c31"),
                    CourseId = Guid.Parse("056529e5-261e-496c-b7d2-1f07e37c155a"),
                    StudentId = "123",
                    Course = new Course()
                    {
                        Id = Guid.Parse("056529e5-261e-496c-b7d2-1f07e37c155a")
                    }
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

            mockRepository.Setup(repo => repo.GetStudentEnrollmentsWithCoursesAsync(It.IsAny<string>()))
                .ReturnsAsync((string studentId) => enrollments.Where(x => x.StudentId == studentId).ToList());

            mockRepository.Setup(repo => repo.IsUserEnrolledInCourseAsync(It.IsAny<string>(), It.IsAny<Guid>()))
                .ReturnsAsync((string userId, Guid courseId) => enrollments.Any(c => c.StudentId == userId && c.CourseId == courseId));

            return mockRepository;
        }
    }
}