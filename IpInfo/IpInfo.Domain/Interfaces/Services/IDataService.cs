using IpInfo.Domain.Result;

namespace IpInfo.Domain.Interfaces.Services
{
    public interface IDataService
    {
        /// <summary>
        /// Сохранение данных в БД.
        ///Возвращает объект BaseResult, чтобы в случае чего можно было проверить 
        ///успешность сохранения данных
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        Task<BaseResult> SaveDataAsync(string Data, string ip);



        /// <summary>
        /// Получение данных в формате string с заданного Uri.
        /// Метод возвращает объект типа BaseResult&lt;string&gt;
        /// Данный объект хранит в себе данные в поле Data,
        /// а также поля ErrorCode и ErrorMessage, которые заполняются
        /// в случае появления ошибок, чтобы в дальнейшем можно было проверить
        /// их наличие проверив данные поля
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        Task<BaseResult<string>> GetDataAsync(string ip);
    }

}
