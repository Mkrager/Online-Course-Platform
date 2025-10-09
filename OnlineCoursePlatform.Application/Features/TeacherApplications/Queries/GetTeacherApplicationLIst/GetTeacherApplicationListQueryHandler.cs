using MediatR;
using OnlineCoursePlatform.Application.Contracts.Application;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Domain.Entities;
using OnlineCoursePlatform.Domain.Enums;

namespace OnlineCoursePlatform.Application.Features.TeacherApplications.Queries.GetTeacherApplicationLIst
{
    public class GetTeacherApplicationListQueryHandler : IRequestHandler<GetTeacherApplicationListQuery, List<TeacherApplicationListVm>>
    {
        private readonly IRequestRepository<TeacherApplication> _requestRepository;
        private readonly IRequestUserNamePopulator _requestUserNamePopulator;
        public GetTeacherApplicationListQueryHandler(IRequestRepository<TeacherApplication> requestRepository, IRequestUserNamePopulator requestUserNamePopulator)
        {
            _requestRepository = requestRepository;
            _requestUserNamePopulator = requestUserNamePopulator;
        }
        public async Task<List<TeacherApplicationListVm>> Handle(GetTeacherApplicationListQuery request, CancellationToken cancellationToken)
        {
            var teacherApplication = await _requestRepository.GetRequestsByStatusAsync(RequestStatus.Pending);

            return await _requestUserNamePopulator.PopulateUserNamesAsync<TeacherApplication, TeacherApplicationListVm>(teacherApplication);
        }
    }
}
