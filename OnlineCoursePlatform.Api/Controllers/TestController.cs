using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineCoursePlatform.Application.Features.Tests.Commands.CreateTest;
using OnlineCoursePlatform.Application.Features.Tests.Queries.GetTestDetail;

namespace OnlineCoursePlatform.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController(IMediator mediator) : Controller
    {

        [HttpPost(Name = "AddTest")]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateTestCommand createTestCommand)
        {
            var id = await mediator.Send(createTestCommand);
            return Ok(id);
        }

        [HttpGet("{id}", Name = "GetTestById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TestDetailVm>> GetTestById(Guid id)
        {
            var getCourseDetailQuery = new GetTestDetailQuery() { Id = id };
            return Ok(await mediator.Send(getCourseDetailQuery));
        }

    }
}
