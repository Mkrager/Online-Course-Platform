using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineCoursePlatform.Application.Features.Lessons.Commands.CreateLesson;
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
    }
}
