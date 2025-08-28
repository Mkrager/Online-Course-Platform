using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineCoursePlatform.Application.Contracts;
using OnlineCoursePlatform.Application.Features.Enrollments.Queries.GetEnrollmentsByStudent;
using OnlineCoursePlatform.Application.Features.Lessons.Queries.GetLessonDetail;

namespace OnlineCoursePlatform.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentController(IMediator mediator, ICurrentUserService currentUserService) : Controller
    {
        [HttpGet(Name = "GetEnrollmentByStudentId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<LessonDetailVm>> GetEnrollmentByUserId()
        {
            var userId = currentUserService.UserId;

            var getEnrollmentByStudentIdQuery = new GetEnrollmentsByStudentQuery() { UserId = userId };
            return Ok(await mediator.Send(getEnrollmentByStudentIdQuery));
        }
    }
}
