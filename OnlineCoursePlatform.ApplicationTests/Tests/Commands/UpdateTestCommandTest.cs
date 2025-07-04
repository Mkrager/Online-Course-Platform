using AutoMapper;
using Moq;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Features.Courses.Commands.UpdateCourse;
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
    }
}
