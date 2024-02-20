using IpInfo.Domain.Interfaces;

namespace IpInfo.Domain.Models
{
    public class IpInfoEntity : IAuditable

    {
        public long Id { get; set; }
        public string IpAddress { get; set; }
        public string InfoData { get; set; }
        public DateTime RequestTime { get; set; }
    }
}
