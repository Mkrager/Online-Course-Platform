using OnlineCoursePlatform.Application.Common.Filters;
using OnlineCoursePlatform.Application.Common.Validators;
using OnlineCoursePlatform.Application.Contracts;
using OnlineCoursePlatform.Application.Contracts.Application;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Exceptions;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Features.TeacherApplications.Commands.UpdateTeacherApplicationStatus.CancelTeacher
{
    public class CancelTeacherApplicationCommandValidator : AccessValidator<CancelTeacherApplicationCommand, IRequestRepository<TeacherApplication>>
    {
        public CancelTeacherApplicationCommandValidator(IRequestRepository<TeacherApplication> service, IPermissionService permissionService, string? errorMessage = null) : base(service, permissionService, errorMessage)
        {
        }

        protected async override Task<bool> HasAccessInternal(CancelTeacherApplicationCommand model, CancellationToken token)
        {
            var teacherApplication = await _service.GetByIdAsync(model.Id);

            if (teacherApplication == null)
                throw new NotFoundException(nameof(TeacherApplication), model.Id);

            if (teacherApplication.RequestedBy != model.UserId)
                return false;

            return true;
        }
    }
}