using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineCoursePlatform.Application.Contracts;
using OnlineCoursePlatform.Application.Features.TestAttemps.Commands.EndAttempt;
using OnlineCoursePlatform.Application.Features.TestAttemps.Commands.StartAttempt;

namespace OnlineCoursePlatform.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestAttemptController(IMediator mediator, ICurrentUserService currentUserService) : Controller
    {
        [HttpPost(Name = "StartAttempt")]
        public async Task<ActionResult<Guid>> StartAttempt([FromBody] StartAttemptCommand startAttemptCommand)
        {
            startAttemptCommand.UserId = currentUserService.UserId;

            var id = await mediator.Send(startAttemptCommand);
            return Ok(id);
        }

        [HttpPut(Name = "EndAttempt")]
        public async Task<ActionResult> EndAttempt([FromBody] EndAttemptCommand endAttemptCommand)
        {
            foreach (var answer in endAttemptCommand.UserAnswerDto)
            {
                answer.UserId = currentUserService.UserId;
            }

            await mediator.Send(endAttemptCommand);
            return NoContent();
        }
    }
}
