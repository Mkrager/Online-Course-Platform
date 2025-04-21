using Microsoft.EntityFrameworkCore;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Persistence
{
    public class OnlineCoursePlatformDbContext : DbContext
    {
        public OnlineCoursePlatformDbContext(DbContextOptions<OnlineCoursePlatformDbContext> options)
            : base(options)
        {
            
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Certificate> Certificates { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Test> Test { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OnlineCoursePlatformDbContext).Assembly);

            var testCategory1Id = Guid.Parse("6f4c7e59-74c7-41c5-9fa7-4b75b7d9f3a3");
            var testCategory2Id = Guid.Parse("6f4c7e59-74c7-41c5-9fa7-4b75b7d9f3a4");

            modelBuilder.Entity<Category>().HasData(new Category
            {
                Id = testCategory1Id,
                Name = "test"
            });

            modelBuilder.Entity<Category>().HasData(new Category
            {
                Id = testCategory2Id,
                Name = "test2"
            });

            modelBuilder.Entity<Course>().HasData(new Course
            {
                Id = Guid.Parse("b8c3f27a-7b28-4ae6-94c2-91fdc33b77e8"),
                CategoryId = testCategory1Id,
                Description = "test",
                IsPublished = true,
                InstructorId = "testId",
                Price = 100,
                Title = "test",
                ThumbnailUrl = "test"
            });

            modelBuilder.Entity<Course>().HasData(new Course
            {
                Id = Guid.Parse("b8c3f27a-7b28-4ae6-94c2-91fdc33b77e2"),
                CategoryId = testCategory1Id,
                Description = "test2",
                IsPublished = true,
                InstructorId = "test2Id",
                Price = 100,
                Title = "test2",
                ThumbnailUrl = "test2"
            });
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<Course>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAt = DateTime.Now;
                        //entry.Entity.CreatedBy = _loggedInUserService.UserId;
                        break;
                    //case EntityState.Modified:
                    //    entry.Entity. = DateTime.Now;
                    //    entry.Entity.LastModifiedBy = _loggedInUserService.UserId;
                    //    break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

    }
}
