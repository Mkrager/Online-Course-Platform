using Microsoft.Extensions.Configuration;
using OnlineCoursePlatform.Application.Contracts.Infrastructure;

namespace OnlineCoursePlatform.Infrastructure.Configuration
{
    public class BaseUrlProvider : IBaseUrlProvider
    {
        private readonly IConfiguration _configuration;

        public BaseUrlProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string BaseUrl => _configuration["App:BaseUrl"];
    }
}
