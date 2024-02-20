namespace IpInfo.Domain.Interfaces.Repositories
{
    public interface IBaseRepository<TEntity> 
    {
        Task<TEntity> CreateAsync(TEntity entity);

    }
}
