using FluentValidation;
using OnlineCoursePlatform.Application.Contracts.Persistance;

namespace OnlineCoursePlatform.Application.Features.Lessons.Queries.GetCourseLessonsList
{
    public class GetCourseLessonsQueryValidator : AbstractValidator<GetCourseLessonsQuery>
    {
        private readonly IEnrollmentRepository _enrollmentRepository;
        public GetCourseLessonsQueryValidator(IEnrollmentRepository enrollmentRepository)
        {
            _enrollmentRepository = enrollmentRepository;

            RuleFor(x => x)
                .MustAsync(async (model, cancellationToken) =>
                    await IsUserEnrolledInCourse(model.UserId, model.CourseId, cancellationToken))
                .WithMessage("You don't have access to this course");
        }

        private async Task<bool> IsUserEnrolledInCourse(string userId, Guid courseId, CancellationToken token)
        {
            return await _enrollmentRepository.IsUserEnrolledInCourseAsync(userId, courseId);
        }
    }
}
