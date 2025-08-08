using AutoMapper;
using Moq;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Features.Courses.Queries.GetCoursesList;
using OnlineCoursePlatform.Application.Profiles;
using OnlineCoursePlatform.Application.UnitTests.Mocks;
using Shouldly;

namespace OnlineCoursePlatform.ApplicationTests.Courses.Queries
{
    public class GetCoursesListQueryHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<ICourseRepository> _mockCourseRepository;

        public GetCoursesListQueryHandlerTest()
        {
            _mockCourseRepository = CourseRepositoryMock.GetCourseRepository();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task GetCourseList_ReturnsListOfCourses()
        {
            var handler = new GetCoursesListQueryHandler(_mapper, _mockCourseRepository.Object);

            var result = await handler.Handle(new GetCoursesListQuery(), CancellationToken.None);

            result.ShouldBeOfType<List<CourseListVm>>();

            result.Count.ShouldBe(4);
        }
    }
}
