using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineCoursePlatform.Application.DTOs.User;
using OnlineCoursePlatform.Application.Features.User.Queries;

namespace OnlineCoursePlatform.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IMediator mediator) : Controller
    {
        [HttpGet("{id}", Name = "GetUserDetails")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserDetailsResponse>> GetUserDetails(string id)
        {
            var getUserDetailQuery = new GetUserDetailsQuery() { Id = id };
            return Ok(await mediator.Send(getUserDetailQuery));
        }
    }
}
