using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineCoursePlatform.Application.Features.TeacherApplications.Commands.CreateTeacherApplication;
using OnlineCoursePlatform.Application.Features.TeacherApplications.Commands.UpdateTeacherApplicationStatus.ApproveTeacher;
using OnlineCoursePlatform.Application.Features.TeacherApplications.Commands.UpdateTeacherApplicationStatus.CancelTeacher;
using OnlineCoursePlatform.Application.Features.TeacherApplications.Commands.UpdateTeacherApplicationStatus.RejectTeacher;
using OnlineCoursePlatform.Application.Features.TeacherApplications.Queries.GetTeacherApplicationLIst;
using OnlineCoursePlatform.Application.Features.TeacherApplications.Queries.GetUserTeacherApplication;

namespace OnlineCoursePlatform.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherApplicationController(IMediator mediator) : Controller
    {
        [Authorize(Roles = "Moderator")]
        [HttpGet(Name = "GetPendingTeacherApplication")]
        public async Task<ActionResult<List<TeacherApplicationListVm>>> GetPendingTeacherApplication()
        {
            var dtos = await mediator.Send(new GetTeacherApplicationListQuery());
            return dtos;
        }

        [Authorize(Roles = "Default")]
        [HttpGet("user", Name = "GetUserTeacherApplication")]
        public async Task<ActionResult<List<TeacherApplicationListVm>>> GetUserTeacherApplication()
        {
            var dtos = await mediator.Send(new GetTeacherApplicationByUserQuery());
            return dtos;
        }

        [Authorize(Roles = "Default")]
        [HttpPost(Name = "AddTeacherApplication")]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateTeacherApplicationCommand createTeacherApplicationCommand)
        {
            var id = await mediator.Send(createTeacherApplicationCommand);
            return Ok(id);
        }

        [Authorize(Roles = "Moderator")]
        [HttpPut("approve/{id}", Name = "ApproveTeacherApplication")]
        public async Task<ActionResult<Guid>> Approve(Guid id)
        {
            await mediator.Send(new ApproveTeacherApplicationCommand()
            {
                Id = id
            });
            return NoContent();
        }

        [Authorize(Roles = "Moderator")]
        [HttpPut("reject", Name = "RejectTeacherApplication")]
        public async Task<ActionResult<Guid>> Reject([FromBody] RejectTeacherApplicationCommand rejectTeacherApplicationCommand)
        {
            await mediator.Send(rejectTeacherApplicationCommand);
            return NoContent();
        }

        [Authorize(Roles = "Default")]
        [HttpPut("cancel/{id}", Name = "CancelTeacherApplication")]
        public async Task<ActionResult<Guid>> Cancel(Guid id)
        {
            await mediator.Send(new CancelTeacherApplicationCommand()
            {
                Id = id,
            });
            return NoContent();
        }
    }
}