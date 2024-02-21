using IpInfo.Domain.Interfaces;
using System.Text.Json;

namespace IpInfo.Domain.Models
{
    public class IpInfoEntity : IAuditable

    {
        public long Id { get; set; }
        public string IpAddress { get; set; }
        public JsonDocument InfoData { get; set; }
        public DateTime RequestTime { get; set; }
    }
}
