using OnlineCoursePlarform.Api.IntegrationTests.Base;
using OnlineCoursePlatform.Application.Features.Levels.Queries.GetLevelsList;
using System.Text.Json;

namespace OnlineCoursePlarform.Api.IntegrationTests.Controllers
{
    public class LevelControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly CustomWebApplicationFactory<Program> _factory;
        public LevelControllerTests(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetAllLevels_ReturnsSuccessAndNonEmptyList()
        {
            var client = _factory.GetAnonymousClient();
            var response = await client.GetAsync("/api/Level");

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<List<LevelListVm>>(responseString);

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.True(result.Count > 0);
        }

    }
}
