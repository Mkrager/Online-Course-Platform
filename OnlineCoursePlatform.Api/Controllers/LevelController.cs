using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineCoursePlatform.Application.Features.Levels.Queries.GetLevelsList;

namespace OnlineCoursePlatform.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LevelController(IMediator mediator) : Controller
    {
        [HttpGet(Name = "GetAllLevels")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<LevelListVm>>> GetAllLevels()
        {
            var dtos = await mediator.Send(new GetLevelsListQuery());
            return Ok(dtos);
        }
    }
}
