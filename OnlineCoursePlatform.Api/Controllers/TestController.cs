using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineCoursePlatform.Application.Features.Tests.Commands.CreateTest;
using OnlineCoursePlatform.Application.Features.Tests.Commands.DeleteTest;
using OnlineCoursePlatform.Application.Features.Tests.Commands.UpdateTest;
using OnlineCoursePlatform.Application.Features.Tests.Queries.GetTestDetail;
using OnlineCoursePlatform.Application.Features.Tests.Queries.GetUserTestsList;

namespace OnlineCoursePlatform.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController(IMediator mediator) : Controller
    {


        [HttpGet("{id}", Name = "GetTestById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TestDetailVm>> GetTestById(Guid id)
        {
            var getCourseDetailQuery = new GetTestDetailQuery() { Id = id };
            return Ok(await mediator.Send(getCourseDetailQuery));
        }

        [HttpGet("by-lesson/{lessonId}", Name = "GetTestsByLessonId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<LessonTestListVm>>> GetTestByLessonId(Guid lessonId)
        {
            var getLessonTestQuery = new GetLessonTestsQuery() { LessonId = lessonId };
            return Ok(await mediator.Send(getLessonTestQuery));
        }

        [HttpPost(Name = "AddTest")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateTestCommand createTestCommand)
        {
            var id = await mediator.Send(createTestCommand);
            return Ok(id);
        }

        [HttpPut(Name = "UpdateTest")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateTest([FromBody] UpdateTestCommand updateTestCommand)
        {
            await mediator.Send(updateTestCommand);
            return NoContent();
        }

        [HttpDelete("{id}", Name = "DeleteTest")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> DeleteTest(Guid id)
        {
            var deleteTestCommand = new DeleteTestCommand() { Id = id };
            await mediator.Send(deleteTestCommand);
            return NoContent();
        }
    }
}
