using OnlineCoursePlatform.Application.Common.Validators;
using OnlineCoursePlatform.Application.Contracts.Application;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Features.TeacherApplications.Commands.UpdateTeacherApplicationStatus.CancelTeacher
{
    public class CancelTeacherApplicationCommandValidator : RequestAccessValidator<CancelTeacherApplicationCommand, TeacherApplication>
    {
        public CancelTeacherApplicationCommandValidator(IRequestRepository<TeacherApplication> service, IPermissionService permissionService, string? errorMessage = null) : base(service, permissionService, errorMessage)
        {
        }

        protected override Guid GetId(CancelTeacherApplicationCommand request) => request.Id;
    }
}