using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineCoursePlatform.Application.Contracts;
using OnlineCoursePlatform.Application.Features.User.Commands.AssignRole;
using OnlineCoursePlatform.Application.Features.User.Queries.GetUserDetails;

namespace OnlineCoursePlatform.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IMediator mediator, ICurrentUserService currentUserService) : Controller
    {
        [HttpGet("teacher", Name = "GetTeacherDetails")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserDetailsVm>> GetTeacherDetails()
        {
            var getUserDetailQuery = new GetUserDetailsQuery() { Id = currentUserService.UserId };

            var result = await mediator.Send(getUserDetailQuery);

            result.Courses = mediator.Send(GetUs)

            return Ok();
        }

        [HttpGet("default", Name = "GetDefaultUserDetails")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserDetailsVm>> GetDefaultUserDetails()
        {
            var getUserDetailQuery = new GetUserDetailsQuery() { Id = currentUserService.UserId };
            return Ok(await mediator.Send(getUserDetailQuery));
        }

        [HttpPut("assign-role", Name = "AssignRole")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> AssignRole(AssignRoleCommand assignRoleCommand)
        {
            await mediator.Send(assignRoleCommand);
            return NoContent();
        }
    }
}
