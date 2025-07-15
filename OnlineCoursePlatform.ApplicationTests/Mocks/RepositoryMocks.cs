using Moq;
using OnlineCoursePlatform.Application.Contracts.Identity;
using OnlineCoursePlatform.Application.Contracts.Infrastructure;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.DTOs.Authentication;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.UnitTests.Mocks
{
    public class RepositoryMocks
    {
        public static Mock<ICourseRepository> GetCourseRepository()
        {
            var courses = new List<Course>
            {
                new Course {Id = Guid.Parse("b8c3f27a-7b28-4ae6-94c2-91fdc33b77e8"), Title = "test", Description = "test", Price = 100, ThumbnailUrl = "test", CategoryId = Guid.Parse("2d6e6fbe-3d9f-4a75-a262-2f2b197b4c6a") },
                new Course { Id = Guid.Parse("b8c3f27a-7b28-4ae6-94c2-91fdc33b77e2"), Title = "test2", Description = "test2", Price = 200, ThumbnailUrl = "test2" }

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

            mockRepository.Setup(r => r.GetAllWithCategoryAndLevel())
                .ReturnsAsync(courses);

            mockRepository.Setup(r => r.GetCoursesByCategoryId(It.IsAny<Guid>()))
                .ReturnsAsync((Guid categoryId) => courses.Where(x => x.CategoryId == categoryId).ToList());

            return mockRepository;
        }
        public static Mock<IAsyncRepository<Level>> GetLevelRepository()
        {
            var levels = new List<Level>
            {
                new Level { Id = Guid.Parse("b8c3f27a-7b28-4ae6-94c2-91fdc33b77e8"), Name = "test" },
                new Level { Id = Guid.Parse("b8c3f27a-7b28-4ae6-94c2-91fdc33b77e2"), Name = "test" }

            };

            var mockRepository = new Mock<IAsyncRepository<Level>>();

            mockRepository.Setup(r => r.ListAllAsync())
                .ReturnsAsync(levels);

            mockRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid id) => levels.FirstOrDefault(x => x.Id == id));

            mockRepository.Setup(r => r.AddAsync(It.IsAny<Level>()))
                .ReturnsAsync((Level level) =>
                {
                    levels.Add(level);
                    return level;
                });

            mockRepository.Setup(r => r.UpdateAsync(It.IsAny<Level>()))
                .Callback((Level level) =>
                {
                    var oldLevel = levels.FirstOrDefault(x => x.Id == level.Id);
                    if (oldLevel != null)
                    {
                        oldLevel.Name = level.Name;
                    }
                });

            mockRepository.Setup(r => r.DeleteAsync(It.IsAny<Level>()))
                .Callback((Level level) => levels.Remove(level));

            return mockRepository;
        }



        public static Mock<ICategoryRepository> GetCategoryRepository()
        {
            var categories = new List<Category>
            {
                new Category { Id = Guid.Parse("3f2a3a3e-27c9-4b65-bfb4-2b1e3d4b54ee"), Name = "TestCategory1" },
                new Category { Id = Guid.Parse("c1e9a0b2-5f3d-4427-8a3f-6db42c948ce4"), Name = "TestCategory2" }

            };

            var mockRepository = new Mock<ICategoryRepository>();

            mockRepository.Setup(r => r.ListAllAsync())
                .ReturnsAsync(categories);

            mockRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid id) => categories.FirstOrDefault(x => x.Id == id));

            mockRepository.Setup(r => r.AddAsync(It.IsAny<Category>()))
                .ReturnsAsync((Category category) =>
                {
                    categories.Add(category);
                    return category;
                });

            mockRepository.Setup(r => r.UpdateAsync(It.IsAny<Category>()))
                .Callback((Category category) =>
                {
                    var oldCategory = categories.FirstOrDefault(x => x.Id == category.Id);
                    if (oldCategory != null)
                    {
                        oldCategory.Name = category.Name;
                    }
                });

            mockRepository.Setup(r => r.DeleteAsync(It.IsAny<Category>()))
                .Callback((Category category) => categories.Remove(category));

            mockRepository.Setup(repo => repo.GetCategoriesWithCourses()).ReturnsAsync(categories);

            mockRepository.Setup(repo => repo.IsCategoryNameUnique(It.IsAny<string>()))
                .ReturnsAsync((string name) => !categories.Any(c => c.Name == name));

            return mockRepository;
        }

        public static Mock<ITestRepository> GetTestRepository()
        {
            var tests = new List<Test>
            {
                new Test { Id = Guid.Parse("3f2a3a3e-27c9-4b65-bfb4-2b1e3d4b54ee"), Title = "Test1", LessonId = Guid.Parse("3f29e1a5-67b4-47f2-a726-05e45bdb2b4c") },
                new Test { Id = Guid.Parse("c1e9a0b2-5f3d-4427-8a3f-6db42c948ce4"), Title = "Test2" }

            };

            var mockRepository = new Mock<ITestRepository>();

            mockRepository.Setup(r => r.ListAllAsync())
                .ReturnsAsync(tests);

            mockRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid id) => tests.FirstOrDefault(x => x.Id == id));

            mockRepository.Setup(r => r.AddAsync(It.IsAny<Test>()))
                .ReturnsAsync((Test test) =>
                {
                    tests.Add(test);
                    return test;
                });

            mockRepository.Setup(r => r.UpdateAsync(It.IsAny<Test>()))
                .Callback((Test test) =>
                {
                    var oldTest = tests.FirstOrDefault(x => x.Id == test.Id);
                    if (oldTest != null)
                    {
                        oldTest.Title = test.Title;
                    }
                });

            mockRepository.Setup(r => r.DeleteAsync(It.IsAny<Test>()))
                .Callback((Test test) => tests.Remove(test));

            mockRepository.Setup(repo => repo.GetTestWithQuestionsAndAnswers(It.IsAny<Guid>())).ReturnsAsync((Guid id) => tests.FirstOrDefault(x => x.Id == id));

            mockRepository.Setup(repo => repo.GetTestsByLessonId(It.IsAny<Guid>())).ReturnsAsync((Guid lessonId) => tests.Where(t => t.LessonId == lessonId).ToList());

            return mockRepository;

        }

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

            mockRepository.Setup(r => r.GetCourseLessons(It.IsAny<Guid>()))
                .ReturnsAsync((Guid courseId) => lessons.Where(x => x.CourseId == courseId).ToList());

            return mockRepository;
        }

        public static Mock<IAsyncRepository<TestAttempt>> GetTestAttemptRepository()
        {
            var testAttempts = new List<TestAttempt>
            {
                new TestAttempt 
                { 
                    Id = Guid.Parse("9f2b5c3e-6d3e-4c9f-9b57-3a8c4f0b1207"), 
                    IsCompleted = true, 
                    StartTime = DateTime.UtcNow, 
                    EndTime = DateTime.UtcNow.AddMinutes(50),
                    UserId = "testUserId",
                    TestId = Guid.Parse("1d4a7b91-2e1f-4df6-9268-91f3f9f253b4")
                },
                new TestAttempt 
                {
                    Id = Guid.Parse("b7f2e0c8-6e03-4bfc-9d9a-94d8b49aa0f2"),
                    IsCompleted = false,
                    StartTime = DateTime.UtcNow.AddDays(-40),
                    EndTime = DateTime.UtcNow.AddDays(-40).AddMinutes(20),
                    UserId = "testUserId2",
                    TestId = Guid.Parse("fa9db1a4-c3cd-4e5b-805f-d4a624ef34c1")
                }
            };

            var mockRepository = new Mock<IAsyncRepository<TestAttempt>>();

            mockRepository.Setup(r => r.ListAllAsync())
                .ReturnsAsync(testAttempts);

            mockRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid id) => testAttempts.FirstOrDefault(x => x.Id == id));

            mockRepository.Setup(r => r.AddAsync(It.IsAny<TestAttempt>()))
                .ReturnsAsync((TestAttempt testAttempt) =>
                {
                    testAttempts.Add(testAttempt);
                    return testAttempt;
                });

            mockRepository.Setup(r => r.UpdateAsync(It.IsAny<TestAttempt>()))
                .Callback((TestAttempt testAttempt) =>
                {
                    var oldTestAttempt = testAttempts.FirstOrDefault(x => x.Id == testAttempt.Id);
                    if (oldTestAttempt != null)
                    {
                        oldTestAttempt.EndTime = testAttempt.EndTime;
                        oldTestAttempt.IsCompleted = testAttempt.IsCompleted;
                    }
                });

            return mockRepository;
        }

        public static Mock<IUserAnswerRepository> GetUserAnswerRepository()
        {
            var mockRepository = new Mock<IUserAnswerRepository>();

            mockRepository.Setup(repo => repo.PopulateIsCorrectAsync(It.IsAny<List<UserAnswer>>()))
                .ReturnsAsync((List<UserAnswer> userAnswers) =>
                {
                    foreach (var ua in userAnswers)
                    {
                        ua.IsCorrect = true;
                    }
                    return userAnswers;
                });

            return mockRepository;
        }

        public static Mock<IAuthenticationService> GetAuthenticationService()
        {
            var mockService = new Mock<IAuthenticationService>();

            mockService.Setup(service => service.AuthenticateAsync(It.IsAny<AuthenticationRequest>()))
                .ReturnsAsync(new AuthenticationResponse
                {
                    Token = "fake-token",
                    Email = "fake-email",
                    Id = "fake-id",
                    UserName = "fake-userName"
                });


            mockService.Setup(service => service.RegisterAsync(It.IsAny<RegistrationRequest>()))
                .ReturnsAsync("some-id");

            return mockService;
        }

        public static Mock<IPayPalService> GetPayPalService()
        {
            var mockService = new Mock<IPayPalService>();

            mockService.Setup(service => service.CreateOrderAsync(It.IsAny<decimal>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync("some-url");

            return mockService;
        }
    }
}
