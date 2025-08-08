using Moq;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.UnitTests.Mocks
{
    public class LessonRepositoryMock
    {
        public static Mock<ILessonRepository> GetLessonRepository()
        {
            var lessons = new List<Lesson>
            {
                new Lesson { Id = Guid.Parse("b8c3f27a-7b28-4ae6-94c2-91fdc33b77e8"), Title = "test", Description = "test", CourseId = Guid.Parse("7c38fdb6-3e86-4bc2-9c8d-bb7a5e1d9b72") },
                new Lesson { Id = Guid.Parse("b8c3f27a-7b28-4ae6-94c2-91fdc33b77e2"), Title = "test2", Description = "test2" }

            };

            var mockRepository = new Mock<ILessonRepository>();

            mockRepository.Setup(r => r.ListAllAsync())
                .ReturnsAsync(lessons);

            mockRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid id) => lessons.FirstOrDefault(x => x.Id == id));

            mockRepository.Setup(r => r.AddAsync(It.IsAny<Lesson>()))
                .ReturnsAsync((Lesson lesson) =>
                {
                    lessons.Add(lesson);
                    return lesson;
                });

            mockRepository.Setup(r => r.UpdateAsync(It.IsAny<Lesson>()))
                .Callback((Lesson lesson) =>
                {
                    var oldLesson = lessons.FirstOrDefault(x => x.Id == lesson.Id);
                    if (oldLesson != null)
                    {
                        oldLesson.Title = lesson.Title;
                        oldLesson.Description = lesson.Description;
                        oldLesson.CourseId = lesson.CourseId;
                    }
                });

            mockRepository.Setup(r => r.DeleteAsync(It.IsAny<Lesson>()))
                .Callback((Lesson lesson) => lessons.Remove(lesson));

            mockRepository.Setup(r => r.GetLessonsByCourseIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid courseId) => lessons.Where(x => x.CourseId == courseId).ToList());

            return mockRepository;
        }
    }
}
