using AutoMapper;
using MediatR;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Features.TeacherApplications.Queries.GetTeacherApplicationLIst;
using OnlineCoursePlatform.Domain.Entities;
using OnlineCoursePlatform.Domain.Enums;

namespace OnlineCoursePlatform.Application.Features.TeacherApplications.Queries.GetPendingTeacherApplicationByUser
{
    public class GetPendingTeacherApplicationByUserQueryHandler : IRequestHandler<GetPendingTeacherApplicationByUserQuery, List<TeacherApplicationListVm>>
    {
        private readonly IMapper _mapper;
        private readonly IRequestRepository<TeacherApplication> _teacherApplicationRepository;
        public GetPendingTeacherApplicationByUserQueryHandler(IMapper mapper, IRequestRepository<TeacherApplication> teacherApplicationRepository)
        {
            _mapper = mapper;
            _teacherApplicationRepository = teacherApplicationRepository;
        }
        public async Task<List<TeacherApplicationListVm>> Handle(GetPendingTeacherApplicationByUserQuery request, CancellationToken cancellationToken)
        {
            var teacherApplications = await _teacherApplicationRepository.GetRequestsByUserIdAndStatusAsync(request.UserId, RequestStatus.Pending);
            return _mapper.Map<List<TeacherApplicationListVm>>(teacherApplications);
        }
    }
}
