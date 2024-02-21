namespace IpInfo.Domain.Interfaces.Repositories
{
    public interface IBaseRepository<TEntity> 
    {
        /// <summary>
        ///Единственный метод для сохранения данных 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<TEntity> CreateAsync(TEntity entity);

    }
}
