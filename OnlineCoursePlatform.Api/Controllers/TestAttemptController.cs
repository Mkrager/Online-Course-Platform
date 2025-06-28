using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineCoursePlatform.Application.Features.TestAttemps.Commands.StartAttempt;

namespace OnlineCoursePlatform.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestAttemptController(IMediator mediator) : Controller
    {
        [HttpPost]
        public async Task<ActionResult<Guid>> StartAttempt([FromBody] StartAttemptCommand startAttemptCommand)
        {
            var id = await mediator.Send(startAttemptCommand);
            return Ok(id);
        }
    }
}
