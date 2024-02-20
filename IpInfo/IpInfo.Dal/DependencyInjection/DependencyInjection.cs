using IpInfo.Dal.Interceptors;
using IpInfo.Dal.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using IpInfo.Domain.Interfaces.Repositories;
using IpInfo.Dal.Services;
using IpInfo.Domain.Interfaces.Services;
using IpInfo.Domain.Models;
using IpInfo.Domain.Interfaces;

namespace IpInfo.Dal.DependencyInjection
{
    public static class DependencyInjection
    {
        public static void AddDataAccessLayer(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionStringPostgres = configuration.GetConnectionString("PostgresSQL");

            services.AddSingleton<DateInterceptor>();
            var connectionStringHttp = configuration.GetConnectionString("ConnectionAdress");

            services.AddSingleton<IConnectionAdressConfig>
                (new ConnectinAdressConfig { ConnectionString = connectionStringHttp });

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(connectionStringPostgres);
            });
            services.InitRepositories();
            services.InitServices();
        }


        private static void InitRepositories(this IServiceCollection services)
        {
            services.AddScoped<IBaseRepository<IpInfoEntity>, BaseRepository<IpInfoEntity>>();
        }

        private static void InitServices(this IServiceCollection services)
        {
            services.AddHttpClient<IHttpApiClient, HttpApiClient>();
        }


    }
}
