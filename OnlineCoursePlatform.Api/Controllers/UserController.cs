using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineCoursePlatform.Application.Contracts;
using OnlineCoursePlatform.Application.Features.Courses.Queries.GetCoursesByTeacher;
using OnlineCoursePlatform.Application.Features.Enrollments.Queries.GetEnrollmentsByStudent;
using OnlineCoursePlatform.Application.Features.User.Commands.AssignRole.AssignTeacherRole;
using OnlineCoursePlatform.Application.Features.User.Queries.GetUserDetails;

namespace OnlineCoursePlatform.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IMediator mediator, ICurrentUserService currentUserService) : Controller
    {
        [Authorize(Roles = "Teacher")]
        [HttpGet("teacher", Name = "GetTeacherDetails")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserDetailsVm>> GetTeacherDetails()
        {
            var userId = currentUserService.UserId;

            var getUserDetailQuery = new GetUserDetailsQuery() { Id = userId };

            var result = await mediator.Send(getUserDetailQuery);

            result.Courses = await mediator.Send(new GetCoursesByTeacherQuery() { UserId = userId });

            return Ok(result);
        }

        [Authorize(Roles = "Default")]
        [HttpGet("default", Name = "GetDefaultUserDetails")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserDetailsVm>> GetDefaultUserDetails()
        {
            var userId = currentUserService.UserId;

            var getUserDetailQuery = new GetUserDetailsQuery() { Id = userId };

            var result = await mediator.Send(getUserDetailQuery);

            result.Enrollments = await mediator.Send(new GetEnrollmentsByStudentQuery() { UserId = userId });

            return Ok(result);
        }

        [Authorize(Roles = "Moderator")]
        [HttpPut("assign-teacher", Name = "AssignRole")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> AssignTeacherRole(AssignTeacherRoleCommand assignTeacherRoleCommand)
        {
            await mediator.Send(assignTeacherRoleCommand);
            return NoContent();
        }
    }
}