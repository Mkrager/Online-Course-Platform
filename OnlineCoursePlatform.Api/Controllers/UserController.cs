using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineCoursePlatform.Application.Contracts;
using OnlineCoursePlatform.Application.DTOs.User;
using OnlineCoursePlatform.Application.Features.User.Queries;

namespace OnlineCoursePlatform.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IMediator mediator, ICurrentUserService currentUserService) : Controller
    {
        [HttpGet(Name = "GetUserDetails")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserDetailsResponse>> GetUserDetails()
        {
            var getUserDetailQuery = new GetUserDetailsQuery() { Id = currentUserService.UserId };
            return Ok(await mediator.Send(getUserDetailQuery));
        }
    }
}
