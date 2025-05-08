using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineCoursePlatform.Application.Features.Lessons.Commands.CreateLesson;
using OnlineCoursePlatform.Application.Features.Lessons.Commands.DeleteLesson;
using OnlineCoursePlatform.Application.Features.Lessons.Commands.UpdateLesson;
using OnlineCoursePlatform.Application.Features.Lessons.Queries.GetCourseLessonsList;

namespace OnlineCoursePlatform.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonController(IMediator mediator) : Controller
    {

        [HttpPost(Name = "AddLesson")]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateLessonCommand createLessonCommand)
        {
            var id = await mediator.Send(createLessonCommand);
            return Ok(id);
        }

        [HttpGet("{courseId}", Name = "GetCourseLessons")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<CourseLessonListVm>>> GetCourseLessons(Guid courseId)
        {
            var getCourseLessonsQuery = new GetCourseLessonsQuery() { CourseId = courseId };
            return Ok(await mediator.Send(getCourseLessonsQuery));
        }

        [HttpPut(Name = "UpdateLesson")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> UpdateLesson([FromBody] UpdateLessonCommand updateLessonCommand)
        {
            await mediator.Send(updateLessonCommand);
            return NoContent();
        }

        [HttpDelete("{id}", Name = "DeleteLesson")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> DeleteLesson(Guid id)
        {
            var deleteLessonCommand = new DeleteLessonCommand() { Id = id };
            await mediator.Send(deleteLessonCommand);
            return NoContent();
        }
    }
}
