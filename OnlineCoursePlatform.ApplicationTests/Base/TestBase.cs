using AutoMapper;
using OnlineCoursePlatform.Application.Profiles;

namespace OnlineCoursePlatform.Application.UnitTests.Base
{
    public abstract class TestBase
    {
        protected readonly IMapper _mapper;

        protected TestBase()
        {
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configurationProvider.CreateMapper();
        }
    }
}