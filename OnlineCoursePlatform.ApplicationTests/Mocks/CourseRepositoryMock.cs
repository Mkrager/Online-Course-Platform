using Moq;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.UnitTests.Mocks
{
    public class CourseRepositoryMock
    {
        public static Mock<ICourseRepository> GetCourseRepository()
        {
            var courses = new List<Course>
            {
                new Course 
                {
                    Id = Guid.Parse("b8c3f27a-7b28-4ae6-94c2-91fdc33b77e8"), 
                    Title = "test", 
                    Description = "test", 
                    Price = 100, 
                    ThumbnailUrl = "test", 
                    CategoryId = Guid.Parse("2d6e6fbe-3d9f-4a75-a262-2f2b197b4c6a") 
                },
                new Course 
                { 
                    Id = Guid.Parse("b8c3f27a-7b28-4ae6-94c2-91fdc33b77e2"), 
                    Title = "test2", 
                    Description = "test2", 
                    Price = 200,
                    ThumbnailUrl = "test2" 
                },
                new Course
                {
                    Id = Guid.Parse("b8c3f27a-7b28-4ae6-94c2-91fdc33b21e8"),
                    Title = "test3",
                    Description = "test4",
                    Price = 100, 
                    ThumbnailUrl = "test",
                    CategoryId = Guid.Parse("2d6e6fbe-3d9f-4a75-a261-2f2b197b4c6a"),
                    CreatedBy = "id"
                },
                new Course
                {
                    Id = Guid.Parse("b8c3f27a-7b28-5ae6-94c2-91fdc33b77e2"),
                    Title = "test2",
                    Description = "test5",
                    Price = 200,
                    ThumbnailUrl = "test2",
                    CreatedBy = "id"
                }
            };

            var mockRepository = new Mock<ICourseRepository>();

            mockRepository.Setup(r => r.ListAllAsync())
                .ReturnsAsync(courses);

            mockRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid id) => courses.FirstOrDefault(x => x.Id == id));

            mockRepository.Setup(r => r.AddAsync(It.IsAny<Course>()))
                .ReturnsAsync((Course course) =>
                {
                    courses.Add(course);
                    return course;
                });

            mockRepository.Setup(r => r.UpdateAsync(It.IsAny<Course>()))
                .Callback((Course course) =>
                {
                    var oldCourse = courses.FirstOrDefault(x => x.Id == course.Id);
                    if (oldCourse != null)
                    {
                        oldCourse.Title = course.Title;
                        oldCourse.Price = course.Price;
                        oldCourse.ThumbnailUrl = course.ThumbnailUrl;
                        oldCourse.CategoryId = course.CategoryId;
                        oldCourse.Description = course.Description;
                    }
                });

            mockRepository.Setup(r => r.DeleteAsync(It.IsAny<Course>()))
                .Callback((Course course) => courses.Remove(course));

            mockRepository.Setup(r => r.GetCoursesWithCategoryAndLevelAsync())
                .ReturnsAsync(courses);

            mockRepository.Setup(r => r.GetCoursesByCategoryIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid categoryId) => courses.Where(x => x.CategoryId == categoryId).ToList());

            mockRepository.Setup(r => r.GetCoursesByUserIdAsync(It.IsAny<string>()))
                .ReturnsAsync((string userId) => courses.Where(x => x.CreatedBy == userId).ToList());

            mockRepository.Setup(r => r.UpdateIsPublishedAsync(It.IsAny<Course>(), It.IsAny<bool>()))
                .Callback((Course course, bool isPublish) =>
                {
                    course.IsPublished = isPublish;
                });

            return mockRepository;
        }
    }
}