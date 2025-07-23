using Microsoft.EntityFrameworkCore;
using OnlineCoursePlatform.Application.Contracts;
using OnlineCoursePlatform.Domain.Common;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Persistence
{
    public class OnlineCoursePlatformDbContext : DbContext
    {
        private readonly ICurrentUserService _currentUserService;
        public OnlineCoursePlatformDbContext(DbContextOptions<OnlineCoursePlatformDbContext> options, ICurrentUserService currentUserService)
            : base(options)
        {
            _currentUserService = currentUserService;
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Certificate> Certificates { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<Level> Levels { get; set; }
        public DbSet<UserAnswer> UserAnswers { get; set; }
        public DbSet<TestAttempt> TestAttempts { get; set; }
        public DbSet<Payment> Payments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OnlineCoursePlatformDbContext).Assembly);


            modelBuilder.Entity<UserAnswer>()
                .HasOne(ua => ua.Answer)
                .WithMany()
                .HasForeignKey(ua => ua.AnswerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserAnswer>()
                .HasOne(ua => ua.Question)
                .WithMany()
                .HasForeignKey(ua => ua.QuestionId)
                .OnDelete(DeleteBehavior.Restrict);

            var testCategory1Id = Guid.Parse("6f4c7e59-74c7-41c5-9fa7-4b75b7d9f3a3");
            var testCategory2Id = Guid.Parse("6f4c7e59-74c7-41c5-9fa7-4b75b7d9f3a4");

            modelBuilder.Entity<Level>().HasData(new Level
            {
                Id = Guid.Parse("f80e97ef-6640-41a5-8ccd-603a6ab1bd33"),
                Order = 1,
                Name = "Beginner"
            });

            modelBuilder.Entity<Level>().HasData(new Level
            {
                Id = Guid.Parse("03e986cf-2784-4096-b130-2762c2018777"),
                Order = 2,
                Name = "Intermediate"
            });

            modelBuilder.Entity<Level>().HasData(new Level
            {
                Id = Guid.Parse("3503ccbe-92df-4525-a908-a4aceeae1036"),
                Order = 3,
                Name = "Advanced"
            });

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
                CreatedBy = "testId",
                LevelId = Guid.Parse("f80e97ef-6640-41a5-8ccd-603a6ab1bd33"),
                Price = 100,
                Title = "test",
                ThumbnailUrl = "test"
            });

            modelBuilder.Entity<Course>().HasData(new Course
            {
                Id = Guid.Parse("7e1e9e74-905f-4ad6-8f8d-26ab9dd98ec1"),
                CategoryId = testCategory1Id,
                Description = "test2",
                IsPublished = true,
                LevelId = Guid.Parse("f80e97ef-6640-41a5-8ccd-603a6ab1bd33"),
                CreatedBy = "test2Id",
                Price = 100,
                Title = "test2",
                ThumbnailUrl = "test2"
            });

            modelBuilder.Entity<Lesson>().HasData(new Lesson
            {
                Id = Guid.Parse("2e8b13d5-4c5e-4f4b-9387-8e19c844dbe9"),
                CourseId = Guid.Parse("7e1e9e74-905f-4ad6-8f8d-26ab9dd98ec1"),
                Title = "testLesson"
            });

            modelBuilder.Entity<Lesson>().HasData(new Lesson
            {
                Id = Guid.Parse("9c7f3d18-2c1e-4f37-9843-b25b6f1bfe49"),
                CourseId = Guid.Parse("7e1e9e74-905f-4ad6-8f8d-26ab9dd98ec1"),
                Title = "test2Lesson"
            });


            modelBuilder.Entity<Test>().HasData(new Test
            {
                Id = Guid.Parse("1f5a4c21-2c9b-4b4e-bcb9-36b770a742d0"),
                LessonId = Guid.Parse("9c7f3d18-2c1e-4f37-9843-b25b6f1bfe49"),
                Title = "test"
            });

            modelBuilder.Entity<Test>().HasData(new Test
            {
                Id = Guid.Parse("4a8c1a3f-7e1c-49d3-9bc1-1f8b38f1f3aa"),
                LessonId = Guid.Parse("9c7f3d18-2c1e-4f37-9843-b25b6f1bfe49"),
                Title = "test2"
            });

            modelBuilder.Entity<Question>().HasData(new Question
            {
                Id = Guid.Parse("a1783ff1-7a2b-4d7a-84a5-c453be4c0f90"),
                TestId = Guid.Parse("4a8c1a3f-7e1c-49d3-9bc1-1f8b38f1f3aa"),
                Text = "someQuestion"
            });

            modelBuilder.Entity<Answer>().HasData(new Answer
            {
                Id = Guid.Parse("5cd711f0-cc43-4b7f-b6a3-d7f4c208b38a"),
                IsCorrect = true,
                QuestionId = Guid.Parse("a1783ff1-7a2b-4d7a-84a5-c453be4c0f90"),
                Text = "someAnswer"
            });

            modelBuilder.Entity<TestAttempt>().HasData(new TestAttempt
            {
                Id = Guid.Parse("d45f7a9e-3a01-4c64-9f86-cde3e55ebc36"),
                StartTime = DateTime.Now,
                IsCompleted = false,
                TestId = Guid.Parse("4a8c1a3f-7e1c-49d3-9bc1-1f8b38f1f3aa"),
                UserId = "someUserId",              
            });
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.UtcNow;
                        entry.Entity.CreatedBy = _currentUserService.UserId;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedDate = DateTime.UtcNow;
                        entry.Entity.LastModifiedBy = _currentUserService.UserId;
                        break;
                }
            }

            foreach (var entry in ChangeTracker.Entries<TimestampedEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAt = DateTime.UtcNow;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedDate = DateTime.UtcNow;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
