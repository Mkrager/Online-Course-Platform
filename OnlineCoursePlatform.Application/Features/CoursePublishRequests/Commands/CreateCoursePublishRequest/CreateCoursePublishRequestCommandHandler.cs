using AutoMapper;
using MediatR;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Features.CoursePublishRequests.Commands.CreateCoursePublishRequest
{
    public class CreateCoursePublishRequestCommandHandler : IRequestHandler<CreateCoursePublishRequestCommand, Guid>
    {
        private readonly IAsyncRepository<CoursePublishRequest> _coursePublishRequestRepository;
        private readonly IMapper _mapper;
        public CreateCoursePublishRequestCommandHandler(IAsyncRepository<CoursePublishRequest> coursePublishRequestRepository, IMapper mapper)
        {
            _coursePublishRequestRepository = coursePublishRequestRepository;
            _mapper = mapper;
        }
        public async Task<Guid> Handle(CreateCoursePublishRequestCommand request, CancellationToken cancellationToken)
        {
            var coursePublishRequest = _mapper.Map<CoursePublishRequest>(request);

            coursePublishRequest = await _coursePublishRequestRepository.AddAsync(coursePublishRequest);

            return coursePublishRequest.Id;
        }
    }
}