using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineCoursePlatform.Application.Features.Tests.Commands.CreateTest;

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
    }
}
