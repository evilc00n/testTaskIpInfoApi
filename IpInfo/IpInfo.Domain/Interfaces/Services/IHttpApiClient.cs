namespace IpInfo.Domain.Interfaces.Services
{
    public interface IHttpApiClient
    {
        
        /// <summary>
        /// Метод для получения данных с указанного uri
        /// Либо возвращает данные в формате string,
        /// Либо бросает исключение
        /// </summary>
        /// <param name="requestUri"></param>
        /// <returns></returns>
        Task<string> GetAsync(string requestUri);
    }
}
