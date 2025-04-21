using OnlineCoursePlarform.Api.IntegrationTests.Base;
using OnlineCoursePlatform.Application.Features.Courses.Commands.CreateCourse;
using OnlineCoursePlatform.Application.Features.Tests.Commands.CreateTest;
using OnlineCoursePlatform.Domain.Entities;
using System.Net;
using System.Text;
using System.Text.Json;

namespace OnlineCoursePlarform.Api.IntegrationTests.Controllers
{
    public class TestControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly CustomWebApplicationFactory<Program> _factory;
        public TestControllerTests(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task CreateTest_ReturnsSuccessAndValidResponse()
        {
            var client = _factory.GetAnonymousClient();

            var questions = new List<QuestionDto>();

            var createTestCommand = new CreateTestCommand
            {
                Title = "TestCourse",
                CourseId = Guid.Parse("b8c3f27a-7b28-4ae6-94c2-91fdc33b77e8"),
                Questions = questions,
            };

            var content = new StringContent(
                JsonSerializer.Serialize(createTestCommand),
                Encoding.UTF8,
                "application/json"
            );

            var response = await client.PostAsync("/api/Test", content);

            var responseString = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<Guid>(responseString);

            Assert.NotNull(result);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
