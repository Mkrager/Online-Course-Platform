using OnlineCoursePlatform.Domain.Common;

namespace OnlineCoursePlatform.Application.Contracts.Application
{
    public interface IRequestUserNamePopulator
    {
        Task<List<TViewModel>> PopulateUserNamesAsync<TEntity, TViewModel>(List<TEntity> requests)
            where TEntity : RequestEntity
            where TViewModel : class, new();
    }
}