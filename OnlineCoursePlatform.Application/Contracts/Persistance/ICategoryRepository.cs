using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Contracts.Persistance
{
    public interface ICategoryRepository : IAsyncRepository<Category>
    {
        Task<List<Category>> GetCategoriesWithCourses();
        Task<bool> IsCategoryNameUnique(string name); 
    }
}
