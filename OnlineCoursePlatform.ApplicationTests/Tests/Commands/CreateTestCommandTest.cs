using Moq;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Features.Tests.Commands.CreateTest;
using OnlineCoursePlatform.Application.UnitTests.Base;
using OnlineCoursePlatform.Application.UnitTests.Mocks;
using Shouldly;

namespace OnlineCoursePlatform.Application.UnitTests.Tests.Commands
{
    public class CreateTestCommandTest : AccessValidatorBaseTest
    {
        private readonly Mock<ITestRepository> _mockTestRepository;

        public CreateTestCommandTest()
        {
            _mockTestRepository = TestRepositoryMock.GetTestRepository();
        }

        [Fact]
        public async Task Should_Create_Test_Successfully()
        {
            var handler = new CreateTestCommandHandler(_mockTestRepository.Object, _mapper);

            var answer = new AnswerDto
            {
                IsCorrect = true,
                Text = "test"
            };

            var question = new QuestionDto
            {
                Text = "Test",
                Answers = new List<AnswerDto> { answer }
            };

            var command = new CreateTestCommand
            {
                Title = "CreatedTest",
                Questions = new List<QuestionDto> { question },
                LessonId = Guid.NewGuid()
            };

            await handler.Handle(command, CancellationToken.None);

            var allCourses = await _mockTestRepository.Object.ListAllAsync();
            allCourses.Count.ShouldBe(3);

            var createdCourse = allCourses.FirstOrDefault(a => a.Title == command.Title);
            createdCourse.ShouldNotBeNull();
            createdCourse.Title.ShouldBe(command.Title);
        }

        [Fact]
        public async Task Validator_ShouldHaveError_WhenQuestionEmpty()
        {
            var validator = new CreateTestCommandValidator(_mockCourseRepository.Object, _mockPermissionService.Object);
            var query = new CreateTestCommand()
            {
                LessonId = Guid.Parse("eede3937-e906-48a8-bb99-cf04c9a19767"),
                Title = "test",
            };

            var result = await validator.ValidateAsync(query);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, f => f.PropertyName == "Questions");
        }

        [Fact]
        public async Task Validator_ShouldHaveError_WhenLessonIdEmpty()
        {
            var validator = new CreateTestCommandValidator(_mockCourseRepository.Object, _mockPermissionService.Object);
            var query = new CreateTestCommand()
            {
                LessonId = Guid.Empty,
                Title = "test",
                Questions = new List<QuestionDto>()
                {
                    new QuestionDto()
                    {
                        Text = "test",
                        Answers = new List<AnswerDto>()
                        {
                             new AnswerDto()
                            {
                                Text = "test",
                                IsCorrect = false
                            }
                        }
                    }
                }
            };

            var result = await validator.ValidateAsync(query);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, f => f.PropertyName == "LessonId");
        }

        [Fact]
        public async Task Validator_ShouldHaveError_WhenQuestionsHasEmptyValue()
        {
            var validator = new CreateTestCommandValidator(_mockCourseRepository.Object, _mockPermissionService.Object);
            var query = new CreateTestCommand()
            {
                LessonId = Guid.Parse("e138ba25-be71-47d2-9c13-a5ac8b0498fd"),
                Title = "test",
                Questions = new List<QuestionDto>()
                {
                    new QuestionDto()
                    {
                        Text = "test",
                        Answers = new List<AnswerDto>()
                        {
                             new AnswerDto()
                            {
                                Text = "",
                                IsCorrect = false
                            }
                        }
                    }
                }
            };

            var result = await validator.ValidateAsync(query);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, f => f.PropertyName == "Questions[0].Answers[0].Text");
        }

        [Fact]
        public async Task Validator_ShouldHaveError_WhenQuestionsHasEmptyAnswers()
        {
            var validator = new CreateTestCommandValidator(_mockCourseRepository.Object, _mockPermissionService.Object);
            var query = new CreateTestCommand()
            {
                LessonId = Guid.Parse("e138ba25-be71-47d2-9c13-a5ac8b0498fd"),
                Title = "test",
                Questions = new List<QuestionDto>()
                {
                    new QuestionDto()
                    {
                        Text = "test"
                    }
                }
            };

            var result = await validator.ValidateAsync(query);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, f => f.PropertyName == "Questions[0].Answers");
        }

        [Fact]
        public async Task Validator_ShouldHaveError_WhenQuestionDontHaveCorrectAnswer()
        {
            var validator = new CreateTestCommandValidator(_mockCourseRepository.Object, _mockPermissionService.Object);
            var query = new CreateTestCommand()
            {
                LessonId = Guid.Parse("e138ba25-be71-47d2-9c13-a5ac8b0498fd"),
                Title = "Test",
                Questions = new List<QuestionDto>()
                {
                    new QuestionDto()
                    {
                        Text = "test",
                        Answers = new List<AnswerDto>()
                        {
                            new AnswerDto()
                            {
                                Text = "test",
                                IsCorrect = false
                            },

                            new AnswerDto()
                            {
                                Text = "test2",
                                IsCorrect = false
                            }
                        }
                    }
                }
            };

            var result = await validator.ValidateAsync(query);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, f => f.PropertyName == "Questions[0].Answers");
        }

    }
}
