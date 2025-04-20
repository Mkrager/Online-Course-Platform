using OnlineCoursePlarform.Api.IntegrationTests.Base;
using OnlineCoursePlatform.Application.Features.Categories.Queries.GetCategoriesList;
using OnlineCoursePlatform.Application.Features.Categories.Queries.GetCategoriesListWithCourses;
using OnlineCoursePlatform.Application.Features.Courses.Queries.GetCoursesList;
using System.Net;
using System.Text.Json;

namespace OnlineCoursePlarform.Api.IntegrationTests.Controllers
{
    public class CategoryControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly CustomWebApplicationFactory<Program> _factory;
        public CategoryControllerTests(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
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

    }
}
