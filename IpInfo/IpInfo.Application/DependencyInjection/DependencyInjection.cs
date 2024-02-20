using IpInfo.Application.Services;
using IpInfo.Domain.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;


namespace IpInfo.Application.DependencyInjection
{
    public static class DependencyInjection
    {
        public static void AddApplications(this IServiceCollection services)
        {

            InitServices(services);

        }

        private static void InitServices(this IServiceCollection services)
        {

            services.AddScoped<IDataService, DataService>();
        }
    }
}
