using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineCoursePlatform.Application.Contracts;
using OnlineCoursePlatform.Application.Features.Tests.Commands.CreateTest;
using OnlineCoursePlatform.Application.Features.Tests.Commands.DeleteTest;
using OnlineCoursePlatform.Application.Features.Tests.Commands.UpdateTest;
using OnlineCoursePlatform.Application.Features.Tests.Queries.GetTestDetail;
using OnlineCoursePlatform.Application.Features.Tests.Queries.GetUserTestsList;

namespace OnlineCoursePlatform.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController(IMediator mediator, ICurrentUserService currentUserService) : Controller
    {

        [HttpGet("{id}", Name = "GetTestById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TestDetailVm>> GetTestById(Guid id)
        {
            var getCourseDetailQuery = new GetTestDetailQuery()
            {
                Id = id,
                UserId = currentUserService.UserId,
                UserRoles = currentUserService.UserRoles
            };
            return Ok(await mediator.Send(getCourseDetailQuery));
        }

        [HttpGet("by-lesson/{lessonId}", Name = "GetTestsByLessonId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<LessonTestListVm>>> GetTestByLessonId(Guid lessonId)
        {
            var getLessonTestQuery = new GetLessonTestsQuery() 
            {
                LessonId = lessonId,
                UserId = currentUserService.UserId,
                UserRoles = currentUserService.UserRoles
            };
            return Ok(await mediator.Send(getLessonTestQuery));
        }

        [Authorize(Roles = "Teacher")]
        [HttpPost(Name = "AddTest")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateTestCommand createTestCommand)
        {
            var id = await mediator.Send(createTestCommand);
            return Ok(id);
        }

        [Authorize(Roles = "Teacher")]
        [HttpPut(Name = "UpdateTest")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateTest([FromBody] UpdateTestCommand updateTestCommand)
        {
            updateTestCommand.UserId = currentUserService.UserId;
            updateTestCommand.UserRoles = currentUserService.UserRoles;

            await mediator.Send(updateTestCommand);
            return NoContent();
        }

        [Authorize(Roles = "Teacher")]
        [HttpDelete("{id}", Name = "DeleteTest")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> DeleteTest(Guid id)
        {
            var deleteTestCommand = new DeleteTestCommand() 
            { 
                Id = id,
                UserId = currentUserService.UserId,
                UserRoles = currentUserService.UserRoles
            };
            await mediator.Send(deleteTestCommand);
            return NoContent();
        }
    }
}
