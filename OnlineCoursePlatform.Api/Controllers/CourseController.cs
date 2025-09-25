using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineCoursePlatform.Application.Contracts;
using OnlineCoursePlatform.Application.Features.Courses.Commands.CreateCourse;
using OnlineCoursePlatform.Application.Features.Courses.Commands.DeleteCourse;
using OnlineCoursePlatform.Application.Features.Courses.Commands.UnPublishCourse;
using OnlineCoursePlatform.Application.Features.Courses.Commands.UpdateCourse;
using OnlineCoursePlatform.Application.Features.Courses.Queries.GetCourseDetail;
using OnlineCoursePlatform.Application.Features.Courses.Queries.GetCoursesByCategory;
using OnlineCoursePlatform.Application.Features.Courses.Queries.GetCoursesList;

namespace OnlineCoursePlatform.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController(IMediator mediator, ICurrentUserService currentUserService) : Controller
    {
        [Authorize(Roles = "Admin")]
        [HttpGet(Name = "GetAllCourses")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<CourseListVm>>> GetAllCourses()
        {
            var role = currentUserService.UserRoles;

            var dtos = await mediator.Send(new GetCoursesListQuery());
            return Ok(dtos);
        }

        [HttpGet("published", Name = "GetPublishedCourses")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<CourseListVm>>> GetPublishedCourses()
        {
            var dtos = await mediator.Send(new GetCoursesListQuery() 
            {
                OnlyPublished = true
            });

            return Ok(dtos);
        }

        [HttpGet("{id}", Name = "GetCourseById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CourseDetailVm>> GetCourseById(Guid id)
        {
            var getCourseDetailQuery = new GetCourseDetailQuery() { Id = id };
            return Ok(await mediator.Send(getCourseDetailQuery));
        }

        [HttpGet("by-category/{categoryId}", Name = "GetCourseByCategoryId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<CoursesByCategoryVm>>> GetCourseByCategoryId(Guid categoryId)
        {
            var getCoursesByCategoryQuery = new GetCoursesByCategoryQuery() { CategoryId = categoryId };
            return Ok(await mediator.Send(getCoursesByCategoryQuery));
        }

        [Authorize(Roles = "Teacher")]
        [HttpPost(Name = "AddCourse")]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateCourseCommand createCourseCommand)
        {
            var id = await mediator.Send(createCourseCommand);
            return Ok(id);
        }

        [Authorize(Roles = "Teacher")]
        [HttpPut(Name = "UpdateCourse")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Update([FromBody] UpdateCourseCommand updateCourseCommand)
        {
            updateCourseCommand.UserId = currentUserService.UserId;

            await mediator.Send(updateCourseCommand);
            return NoContent();
        }

        [Authorize(Roles = "Teacher")]
        [HttpDelete("{id}", Name = "DeleteCourse")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Delete(Guid id)
        {
            var deleteCourseCommand = new DeleteCourseCommand() 
            {
                Id = id,
                UserId = currentUserService.UserId
            };
            await mediator.Send(deleteCourseCommand);
            return NoContent();
        }

        [Authorize(Roles = "Teacher")]
        [HttpPatch("{id}/unpublish", Name = "UnPublishCourse")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> UnPublishCourse(Guid id)
        {
            var unPublishCourseCommand = new UnPublishCourseCommand() 
            { 
                Id = id,
                UserId = currentUserService.UserId
            };
            await mediator.Send(unPublishCourseCommand);
            return NoContent();
        }
    }
}
