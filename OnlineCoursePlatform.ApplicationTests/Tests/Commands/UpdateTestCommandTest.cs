using AutoMapper;
using Moq;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Features.Tests.Commands.CreateTest;
using OnlineCoursePlatform.Application.Features.Tests.Commands.UpdateTest;
using OnlineCoursePlatform.Application.Profiles;
using OnlineCoursePlatform.Application.UnitTests.Mocks;
using Shouldly;

namespace OnlineCoursePlatform.Application.UnitTests.Tests.Commands
{
    public class UpdateTestCommandTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<ITestRepository> _testRepository;

        public UpdateTestCommandTest()
        {
            _testRepository = RepositoryMocks.GetTestRepository();

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task Should_Update_Test_Successfully()
        {
            var handler = new UpdateTestCommandHandler(_mapper, _testRepository.Object);

            var answer = new List<AnswerDto>()
            {
                new AnswerDto
                {
                    Text = "Test",
                    IsCorrect = true
                }
            };

            var question = new List<QuestionDto>()
            {
                new QuestionDto
                {
                    Text = "Test",
                    Answers = answer
                }
            };

            var command = new UpdateTestCommand()
            {
                Id = Guid.Parse("c1e9a0b2-5f3d-4427-8a3f-6db42c948ce4"),
                Title = "updTitle",
                Questions = question
            };

            var result = await handler.Handle(command, CancellationToken.None);

            var updatedTest = await _testRepository.Object.GetTestWithQuestionsAndAnswers(Guid.Parse("c1e9a0b2-5f3d-4427-8a3f-6db42c948ce4"));

            updatedTest.ShouldNotBeNull();
            updatedTest.Title.ShouldBe("updTitle");

            updatedTest.Questions.ShouldNotBeNull();
            updatedTest.Questions.Count.ShouldBe(1);
            updatedTest.Questions.First().Text.ShouldBe("Test");

            var updatedQuestion = updatedTest.Questions.First();
            updatedQuestion.Answers.ShouldNotBeNull();
            updatedQuestion.Answers.Count.ShouldBe(1);
            updatedQuestion.Answers.First().Text.ShouldBe("Test");
            updatedQuestion.Answers.First().IsCorrect.ShouldBeTrue();
        }

        [Fact]
        public async Task Validator_ShouldHaveError_WhenQuestionEmpty()
        {
            var validator = new UpdateTestCommandValidator();
            var query = new UpdateTestCommand()
            {
                Id = Guid.Parse("c1e9a0b2-5f3d-4427-8a3f-6db42c948ce4"),
                LessonId = Guid.Parse("eede3937-e906-48a8-bb99-cf04c9a19767"),
                Title = "test",
            };

            var result = await validator.ValidateAsync(query);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, f => f.PropertyName == "Questions");
        }

        [Fact]
        public async Task Validator_ShouldHaveError_WhenTitleEmpty()
        {
            var validator = new UpdateTestCommandValidator();
            var query = new UpdateTestCommand()
            {
                Id = Guid.Parse("c1e9a0b2-5f3d-4427-8a3f-6db42c948ce4"),
                Title = "",
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
                                IsCorrect = true
                            }
                        }
                    }
                }
            };

            var result = await validator.ValidateAsync(query);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, f => f.PropertyName == "Title");
        }

        [Fact]
        public async Task Validator_ShouldHaveError_WhenQuestionDontHaveCorrectAnswer()
        {
            var validator = new UpdateTestCommandValidator();
            var query = new UpdateTestCommand()
            {
                Id = Guid.Parse("c1e9a0b2-5f3d-4427-8a3f-6db42c948ce4"),
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

        [Fact]
        public async Task Validator_ShouldHaveError_WhenQuestionsHasEmptyValue()
        {
            var validator = new UpdateTestCommandValidator();
            var query = new UpdateTestCommand()
            {
                Id = Guid.Parse("c1e9a0b2-5f3d-4427-8a3f-6db42c948ce4"),
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
            var validator = new UpdateTestCommandValidator();
            var query = new UpdateTestCommand()
            {
                Id = Guid.Parse("c1e9a0b2-5f3d-4427-8a3f-6db42c948ce4"),
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
    }
}
