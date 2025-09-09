using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineCoursePlatform.Application.Features.CoursePublishRequests.Commands.CreateCoursePublishRequest;
using OnlineCoursePlatform.Application.Features.CoursePublishRequests.Commands.UpdateCoursePublishRequestStatus.ApproveCourse;
using OnlineCoursePlatform.Application.Features.CoursePublishRequests.Queries.GetCoursePublishRequestsList;

namespace OnlineCoursePlatform.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursePublishRequestController(IMediator mediator) : Controller
    {
        [HttpPost(Name = "AddCoursePublishrequest")]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateCoursePublishRequestCommand createCoursePublishRequestCommand)
        {
            var id = await mediator.Send(createCoursePublishRequestCommand);
            return Ok(id);
        }

        [HttpPut("approve/{id}", Name = "ApproveCoursePublishRequest")]
        public async Task<ActionResult<Guid>> Approve(Guid id)
        {
            await mediator.Send(new ApproveCoursePublishRequestCommand()
            {
                Id = id
            });
            return NoContent();
        }

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
