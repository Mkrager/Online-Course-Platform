using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineCoursePlatform.Application.Features.CoursePublishRequests.Queries.GetCoursePublishRequestsList;

namespace OnlineCoursePlatform.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursePublishRequestController(IMediator mediator) : Controller
    {
        [HttpGet(Name = "GetAllCoursesRequests")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<CoursePublishRequestsListVm>>> GetAllCoursesRequests()
        {
            var dtos = await mediator.Send(new GetCoursePublishRequestsListQuery());
            return Ok(dtos);
        }
    }
}
