using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineCoursePlatform.Application.Contracts;
using OnlineCoursePlatform.Application.Features.CoursePublishRequests.Commands.CreateCoursePublishRequest;
using OnlineCoursePlatform.Application.Features.CoursePublishRequests.Commands.UpdateCoursePublishRequestStatus.ApproveCourse;
using OnlineCoursePlatform.Application.Features.CoursePublishRequests.Commands.UpdateCoursePublishRequestStatus.CancelCourse;
using OnlineCoursePlatform.Application.Features.CoursePublishRequests.Commands.UpdateCoursePublishRequestStatus.RejectCourse;
using OnlineCoursePlatform.Application.Features.CoursePublishRequests.Queries.GetCoursePublishRequestByUser;
using OnlineCoursePlatform.Application.Features.CoursePublishRequests.Queries.GetCoursePublishRequestsList;
using OnlineCoursePlatform.Domain.Enums;

namespace OnlineCoursePlatform.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursePublishRequestController(IMediator mediator, ICurrentUserService currentUserService) : Controller
    {
        [HttpPost("{courseId}", Name = "AddCoursePublishRequest")]
        public async Task<ActionResult<Guid>> Create(Guid courseId)
        {
            var id = await mediator.Send(new CreateCoursePublishRequestCommand()
            {
                CourseId = courseId
            });
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

        [HttpPut("cancel/{id}", Name = "CancelCoursePublishRequest")]
        public async Task<ActionResult<Guid>> Cancel(Guid id)
        {
            await mediator.Send(new CancelCoursePublishRequestCommand()
            {
                Id = id
            });
            return NoContent();
        }

        [HttpPut("reject", Name = "RejectCoursePublishRequest")]
        public async Task<ActionResult<Guid>> Reject([FromBody] RejectCoursePublishRequestCommand rejectCoursePublishRequestCommand)
        {
            await mediator.Send(rejectCoursePublishRequestCommand);
            return NoContent();
        }

        [HttpGet(Name = "GetAllCoursesRequests")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<CoursePublishRequestsListVm>>> GetAllCoursesRequests([FromQuery] CoursePublishStatus? status)
        {
            var dtos = await mediator.Send(new GetCoursePublishRequestsListQuery() { Status = status });
            return Ok(dtos);
        }

        [HttpGet("user", Name = "GetUserCoursesRequests")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<CoursePublishRequestsListVm>>> GetUserCoursesRequests()
        {
            var dtos = await mediator.Send(new GetCoursePublishRequestByUserQuery()
            {
                UserId = currentUserService.UserId
            });
            return Ok(dtos);
        }
    }
}