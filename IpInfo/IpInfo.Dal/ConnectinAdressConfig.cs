using IpInfo.Domain.Interfaces;

namespace IpInfo.Dal
{
    public class ConnectinAdressConfig : IConnectionAdressConfig
    {
        public string ConnectionString { get; set; }
    }
}
