using OnlineCoursePlarform.Api.IntegrationTests.Base;
using OnlineCoursePlatform.Application.Features.Categories.Queries.GetCategoriesListWithCourses;
using OnlineCoursePlatform.Application.Features.Courses.Commands.CreateCourse;
using OnlineCoursePlatform.Application.Features.Tests.Commands.CreateTest;
using OnlineCoursePlatform.Application.Features.Tests.Queries.GetTestDetail;
using OnlineCoursePlatform.Domain.Entities;
using System.Net;
using System.Text;
using System.Text.Json;
using Xunit.Abstractions;

namespace OnlineCoursePlarform.Api.IntegrationTests.Controllers
{
    public class TestControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly CustomWebApplicationFactory<Program> _factory;
        private readonly ITestOutputHelper _output;
        public TestControllerTests(CustomWebApplicationFactory<Program> factory, ITestOutputHelper output)
        {
            _factory = factory;
            _output = output;
        }

        [Fact]
        public async Task CreateTest_ReturnsSuccessAndValidResponse()
        {
            var client = _factory.GetAnonymousClient();

            var questions = new List<QuestionDto>();

            var createTestCommand = new CreateTestCommand
            {
                Title = "TestCourse",
                CourseId = Guid.Parse("7e1e9e74-905f-4ad6-8f8d-26ab9dd98ec1"),
                Questions = questions,
            };

            var content = new StringContent(
                JsonSerializer.Serialize(createTestCommand),
                Encoding.UTF8,
                "application/json"
            );

            var response = await client.PostAsync("/api/Test", content);

            var responseString = await response.Content.ReadAsStringAsync();
            _output.WriteLine(responseString);
            var result = JsonSerializer.Deserialize<Guid>(responseString);

            Assert.NotNull(result);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }


        [Fact]
        public async Task GetTestWithQuestionAndAnswer_ReturnsSuccessAndNonEmptyResult()
        {
            var client = _factory.GetAnonymousClient();

            var id = Guid.Parse("1f5a4c21-2c9b-4b4e-bcb9-36b770a742d0");

            var response = await client.GetAsync($"/api/test/{id}");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<TestDetailVm>(responseString);

            Assert.NotNull(result);
            Assert.IsType<TestDetailVm>(result);
        }

    }
}
