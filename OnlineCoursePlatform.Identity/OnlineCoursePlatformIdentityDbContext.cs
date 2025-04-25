using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineCoursePlatform.Identity.Models;

namespace OnlineCoursePlatform.Identity
{
    public class OnlineCoursePlatformIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public OnlineCoursePlatformIdentityDbContext()
        {
            
        }

        public OnlineCoursePlatformIdentityDbContext(DbContextOptions<OnlineCoursePlatformIdentityDbContext> options) : base(options)
        {
            
        }
    }
}
