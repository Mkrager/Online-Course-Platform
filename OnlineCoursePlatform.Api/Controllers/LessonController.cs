using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineCoursePlatform.Application.Contracts;
using OnlineCoursePlatform.Application.Features.Lessons.Commands.CreateLesson;
using OnlineCoursePlatform.Application.Features.Lessons.Commands.DeleteLesson;
using OnlineCoursePlatform.Application.Features.Lessons.Commands.UpdateLesson;
using OnlineCoursePlatform.Application.Features.Lessons.Queries.GetCourseLessonsList;
using OnlineCoursePlatform.Application.Features.Lessons.Queries.GetLessonDetail;

namespace OnlineCoursePlatform.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonController(IMediator mediator, ICurrentUserService currentUserService) : Controller
    {
        [Authorize(Roles = "Teacher")]
        [HttpPost(Name = "AddLesson")]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateLessonCommand createLessonCommand)
        {
            var id = await mediator.Send(createLessonCommand);
            return Ok(id);
        }

        [HttpGet("{id}", Name = "GetLessonById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<LessonDetailVm>> GetLessonById(Guid id)
        {
            var getLessonDetailQuery = new GetLessonDetailQuery() 
            { 
                Id = id
            };
            return Ok(await mediator.Send(getLessonDetailQuery));
        }

        [HttpGet("by-course/{courseId}", Name = "GetCourseLessons")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<CourseLessonListVm>>> GetCourseLessons(Guid courseId)
        {
            var getCourseLessonsQuery = new GetCourseLessonsQuery() 
            { 
                CourseId = courseId
            };
            return Ok(await mediator.Send(getCourseLessonsQuery));
        }

        [Authorize(Roles = "Teacher")]
        [HttpPut(Name = "UpdateLesson")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> UpdateLesson([FromBody] UpdateLessonCommand updateLessonCommand)
        {
            await mediator.Send(updateLessonCommand);
            return NoContent();
        }

        [Authorize(Roles = "Teacher")]
        [HttpDelete("{id}", Name = "DeleteLesson")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> DeleteLesson(Guid id)
        {
            var deleteLessonCommand = new DeleteLessonCommand() 
            { 
                Id = id,
            };
            await mediator.Send(deleteLessonCommand);
            return NoContent();
        }
    }
}