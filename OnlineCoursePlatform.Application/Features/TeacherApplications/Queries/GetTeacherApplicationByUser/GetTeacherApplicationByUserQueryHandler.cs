using AutoMapper;
using MediatR;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Features.TeacherApplications.Queries.GetTeacherApplicationLIst;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Features.TeacherApplications.Queries.GetTeacherApplicationByUser
{
    public class GetTeacherApplicationByUserQueryHandler : IRequestHandler<GetTeacherApplicationByUserQuery, List<TeacherApplicationListVm>>
    {
        private readonly IMapper _mapper;
        private readonly IRequestRepository<TeacherApplication> _teacherApplicationRepository;
        public GetTeacherApplicationByUserQueryHandler(IMapper mapper, IRequestRepository<TeacherApplication> teacherApplicationRepository)
        {
            _mapper = mapper;
            _teacherApplicationRepository = teacherApplicationRepository;
        }
        public async Task<List<TeacherApplicationListVm>> Handle(GetTeacherApplicationByUserQuery request, CancellationToken cancellationToken)
        {
            var teacherApplications = await _teacherApplicationRepository.GetRequestByUserIdAsync(request.UserId);
            return _mapper.Map<List<TeacherApplicationListVm>>(teacherApplications);
        }
    }
}
