using AutoMapper;
using Moq;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Features.Courses.Queries.GetCoursesList;
using OnlineCoursePlatform.Application.Profiles;
using OnlineCoursePlatform.ApplicationTests.Mocks;
using OnlineCoursePlatform.Domain.Entities;
using Shouldly;

namespace OnlineCoursePlatform.ApplicationTests.Courses.Queries
{
    public class GetCourseListQueryHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IAsyncRepository<Course>> _mockCourseRepository;

        public GetCourseListQueryHandlerTest()
        {
            _mockCourseRepository = RepositoryMocks.GetCourseRepository();
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

            result.Count.ShouldBe(2);
        }
    }
}
