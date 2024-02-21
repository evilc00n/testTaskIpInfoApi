using IpInfo.Domain.Interfaces.Services;
using System.Text.Json;

namespace IpInfo.Dal.Services
{
    public class HttpApiClient : IHttpApiClient
    {
        private readonly HttpClient _httpClient;

        public HttpApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <inheritdoc/>
        public async Task<string> GetAsync(string requestUri)
        {
            var response = await _httpClient.GetAsync(requestUri);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            return result;
        }
    }
}
