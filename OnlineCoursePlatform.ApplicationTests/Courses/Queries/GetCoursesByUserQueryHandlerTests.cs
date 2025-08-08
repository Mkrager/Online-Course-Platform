using AutoMapper;
using Moq;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Features.Courses.Queries.GetCoursesByTeacher;
using OnlineCoursePlatform.Application.Features.Courses.Queries.GetCoursesList;
using OnlineCoursePlatform.Application.Profiles;
using OnlineCoursePlatform.Application.UnitTests.Mocks;
using Shouldly;

namespace OnlineCoursePlatform.Application.UnitTests.Courses.Queries
{
    public class GetCoursesByTeacherQueryHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<ICourseRepository> _mockCourseRepository;

        public GetCoursesByTeacherQueryHandlerTests()
        {
            _mockCourseRepository = CourseRepositoryMock.GetCourseRepository();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task GetTeacherCourseList_ReturnsListOfTeacherCourses()
        {
            var handler = new GetCoursesByTeacherQueryHandler(_mapper, _mockCourseRepository.Object);

            var result = await handler.Handle(new GetCoursesByTeacherQuery() { UserId = "id"}, CancellationToken.None);

            result.ShouldBeOfType<List<CourseListVm>>();

            result.Count.ShouldBe(2);
        }

    }
}
