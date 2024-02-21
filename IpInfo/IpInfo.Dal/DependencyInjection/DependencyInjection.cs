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


            //задание строки подключения из одного из источников configuration 
            //(например, secret.json или переменная среды)
            var connectionStringPostgres = configuration.GetConnectionString("PostgresSQL");

            //т.к. всё что делает интерцептор - заполняет поле с датой, то регистрируется как синглтон
            services.AddSingleton<DateInterceptor>();

            //аналогично с строкой подключения для БД
            var connectionStringHttp = configuration.GetConnectionString("ConnectionAdress");

            //Сервис для конфигурации http запросов. Конкретно в данном случае в нём содержится лишь поле
            //со строкой подключения, которая нужна для подклчючения к сайту, чтобы не хардкодить её в методе
            //т.к. сервис предоставляет доступ к одному сайту для всех запросов, то объект регистрируется как синглтон 
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
