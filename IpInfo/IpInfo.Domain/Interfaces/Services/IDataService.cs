using IpInfo.Domain.Result;

namespace IpInfo.Domain.Interfaces.Services
{
    public interface IDataService
    {
        /// <summary>
        /// Сохранение данных в БД.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        Task<BaseResult> SaveDataAsync(string Data, string ip);



        /// <summary>
        /// Получение данных в формате string с заданного Uri.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        Task<BaseResult<string>> GetDataAsync(string uri);
    }

}
