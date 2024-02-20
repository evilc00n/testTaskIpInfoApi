namespace IpInfo.Domain.Interfaces.Services
{
    public interface IHttpApiClient
    {
        
        Task<string> GetAsync(string requestUri);
    }
}
