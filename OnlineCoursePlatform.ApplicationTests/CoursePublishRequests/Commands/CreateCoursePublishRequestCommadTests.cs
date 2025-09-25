using Moq;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Features.CoursePublishRequests.Commands.CreateCoursePublishRequest;
using OnlineCoursePlatform.Application.UnitTests.Base;
using OnlineCoursePlatform.Application.UnitTests.Mocks;
using Shouldly;

namespace OnlineCoursePlatform.Application.UnitTests.CoursePublishRequests.Commands
{
    public class CreateCoursePublishRequestCommadTests : AccessValidatorBaseTest
    {
        private readonly Mock<ICoursePublishRequestRepository> _mockCoursePublishRequestRepository;

        public CreateCoursePublishRequestCommadTests()
        {
            _mockCoursePublishRequestRepository = CoursePublishRequestRepositoryMock.GetCoursePublishRequest();
        }

        [Fact]
        public async Task Should_Create_CoursePublishRequest_Successfully()
        {
            var handler = new CreateCoursePublishRequestCommandHandler(_mockCoursePublishRequestRepository.Object, _mapper);

            var command = new CreateCoursePublishRequestCommand() 
            { 
                CourseId = Guid.Parse("370d02a3-01bb-4bd8-842d-b65001a07c3f") 
            };

            var result = await handler.Handle(command, CancellationToken.None);

            var allCategories = await _mockCoursePublishRequestRepository.Object.ListAllAsync();
            allCategories.Count.ShouldBe(3);

            var createdCourse = allCategories.FirstOrDefault(a => a.CourseId == command.CourseId);
            createdCourse.ShouldNotBeNull();
            createdCourse.CourseId.ShouldBe(command.CourseId);
        }

        [Fact]
        public async void Validator_ShouldHaveError_WhenCourseIdEmpty()
        {
            var validator = new CreateCoursePublishRequestCommandValidator(_mockCourseRepository.Object, _mockPermissionService.Object);
            var query = new CreateCoursePublishRequestCommand
            {
                CourseId = Guid.Empty,
            };

            var result = await validator.ValidateAsync(query);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, f => f.PropertyName == "CourseId");
        }
    }
}
