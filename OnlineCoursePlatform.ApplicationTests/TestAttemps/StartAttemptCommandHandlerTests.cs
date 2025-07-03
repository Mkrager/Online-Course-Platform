using AutoMapper;
using Moq;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Profiles;
using OnlineCoursePlatform.ApplicationTests.Mocks;

namespace OnlineCoursePlatform.Application.UnitTests.TestAttemps
{
    public class StartAttemptCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<ITestAttemptRepository> _mockTestAttemptRepository;

        public StartAttemptCommandHandlerTests()
        {
            _mockTestAttemptRepository = RepositoryMocks.GetTestAttemptRepository();

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configurationProvider.CreateMapper();
        }


    }
}
