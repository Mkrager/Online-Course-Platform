using AutoMapper;
using Moq;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Features.CoursePublishRequests.Commands.ApproveCourse;
using OnlineCoursePlatform.Application.Features.CoursePublishRequests.Commands.CreateCoursePublishRequest;
using OnlineCoursePlatform.Application.Profiles;
using OnlineCoursePlatform.Application.UnitTests.Mocks;
using OnlineCoursePlatform.Domain.Entities;
using Shouldly;

namespace OnlineCoursePlatform.Application.UnitTests.CoursePublishRequests.Commands
{
    public class ApproveCoursePublishRequestCommandTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<ICoursePublishRequestRepository> _mockCoursePublishRequestRepository;

        public ApproveCoursePublishRequestCommandTest()
        {
            _mockCoursePublishRequestRepository = CoursePublishRequestRepositoryMock.GetCoursePublishRequest();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task AproveCourseRequest_ValidCommand_UpdatesStatusPropertySuccessfully()
        {
            var handler = new ApproveCoursePublishRequestCommandHandler(_mockCoursePublishRequestRepository.Object);

            var command = new ApproveCoursePublishRequestCommand()
            {
                Id = Guid.Parse("6ba684fb-e3bc-418c-8971-2f302b43daf6")
            };

            var result = await handler.Handle(command, CancellationToken.None);

            var coursePublishRequests = await _mockCoursePublishRequestRepository.Object.ListAllAsync();

            var updCoursePublishRequests = coursePublishRequests.FirstOrDefault(a => a.Id == command.Id);
            updCoursePublishRequests.ShouldNotBeNull();
            updCoursePublishRequests.Id.ShouldBe(command.Id);
        }

    }
}
