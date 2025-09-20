using FluentValidation;
using OnlineCoursePlatform.Application.Contracts.Persistance;

namespace OnlineCoursePlatform.Application.Features.Lessons.Queries.GetCourseLessonsList
{
    public class GetCourseLessonsQueryValidator : AbstractValidator<GetCourseLessonsQuery>
    {
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly ICourseRepository _courseRepository;
        public GetCourseLessonsQueryValidator(IEnrollmentRepository enrollmentRepository, ICourseRepository courseRepository)
        {
            _enrollmentRepository = enrollmentRepository;
            _courseRepository = courseRepository;

            RuleFor(x => x)
                .MustAsync(async (model, cancellationToken) =>
                    await HasAccessToCourse(model.UserId, model.CourseId, cancellationToken))
                .WithMessage("You don't have access to this course");
        }

        private async Task<bool> HasAccessToCourse(string userId, Guid courseId, CancellationToken token)
        {
            if (await _enrollmentRepository.IsUserEnrolledInCourseAsync(userId, courseId))
                return true;

            if (await _courseRepository.IsUserCourseTeacherAsync(userId, courseId))
                return true;

            return false;
        }

    }
}