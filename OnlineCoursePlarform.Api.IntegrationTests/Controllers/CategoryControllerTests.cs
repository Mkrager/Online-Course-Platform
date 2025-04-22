using OnlineCoursePlarform.Api.IntegrationTests.Base;
using OnlineCoursePlatform.Application.Features.Categories.Commands.CreateCategory;
using OnlineCoursePlatform.Application.Features.Categories.Queries.GetCategoriesList;
using OnlineCoursePlatform.Application.Features.Categories.Queries.GetCategoriesListWithCourses;
using OnlineCoursePlatform.Application.Features.Courses.Queries.GetCoursesList;
using System.Net;
using System.Text;
using System.Text.Json;
using Xunit.Abstractions;

namespace OnlineCoursePlarform.Api.IntegrationTests.Controllers
{
    public class CategoryControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly CustomWebApplicationFactory<Program> _factory;
        private readonly ITestOutputHelper _output;
        public CategoryControllerTests(CustomWebApplicationFactory<Program> factory, ITestOutputHelper output)
        {
            _factory = factory;
            _output = output;
        }

        [Fact]
        public async Task GetAllCategories_ReturnsSuccessAndNonEmptyList()
        {
            var client = _factory.GetAnonymousClient();
            var response = await client.GetAsync("/api/Category");

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<List<CategoryListVm>>(responseString);

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.True(result.Count > 0);
        }

        [Fact]
        public async Task GetAllCategoriesWithCourses_ReturnsSuccessAndNonEmptyResult()
        {
            var client = _factory.GetAnonymousClient();

            var response = await client.GetAsync("/api/category/allwithcourses");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<List<CategoryCourseListVm>>(responseString);

            Assert.NotNull(result);
            Assert.IsType<List<CategoryCourseListVm>>(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task CreateCategory_ReturnsSuccessAndValidResponse()
        {
            var client = _factory.GetAnonymousClient();

            var createCategoryCommand = new CreateCategoryCommand
            {
                Name = "TestCategory",
            };

            var content = new StringContent(
                JsonSerializer.Serialize(createCategoryCommand),
                Encoding.UTF8,
                "application/json"
            );

            var response = await client.PostAsync("/api/category", content);
            _output.WriteLine(response.ToString());

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var responseString = await response.Content.ReadAsStringAsync();
            _output.WriteLine(responseString);
            var result = JsonSerializer.Deserialize<Guid>(responseString);

            Assert.NotNull(result);
            Assert.NotEqual(Guid.Empty, result);
        }

        [Fact]
        public async Task DeleteCategory_ReturnsNoContent_WhenCategoryExists()
        {
            var client = _factory.GetAnonymousClient();

            Guid categoryId = Guid.Parse("6f4c7e59-74c7-41c5-9fa7-4b75b7d9f3a4");

            var response = await client.DeleteAsync($"/api/Category/{categoryId}");

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

    }
}
