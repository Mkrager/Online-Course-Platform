using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineCoursePlatform.Application.Features.Lessons.Commands.CreateLesson;

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

    }
}
