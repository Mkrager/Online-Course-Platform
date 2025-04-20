using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineCoursePlatform.Application.Features.Categories.Commands.CreateCategory;
using OnlineCoursePlatform.Application.Features.Categories.Queries.GetCategoriesList;
using OnlineCoursePlatform.Application.Features.Categories.Queries.GetCategoriesListWithCourses;

namespace OnlineCoursePlatform.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController(IMediator mediator) : Controller
    {
        [HttpGet(Name = "GetAllCategories")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<CategoryListVm>>> GetAllCategories()
        {
            var dtos = await mediator.Send(new GetCategoriesListQuery());
            return Ok(dtos);
        }

        [HttpGet("allwithcourses", Name = "GetCategoriesWithCourses")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<CategoryListVm>>> GetCategoriesWithCourses()
        {
            var dtos = await mediator.Send(new GetCategoriesListWithCoursesQuery());
            return Ok(dtos);
        }

        [HttpPost(Name = "AddCategory")]
        public async Task<ActionResult<Guid>> CreateCategory
            ([FromBody] CreateCategoryCommand createCategoryCommand)
        {
            var responce = await mediator.Send(createCategoryCommand);
            return Ok(responce);
        }

    }
}
