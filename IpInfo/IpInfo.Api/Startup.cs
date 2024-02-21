using Asp.Versioning;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace IpInfo.Api
{
    public static class Startup
    {
        /// <summary>
        /// Подключение Swagger
        /// </summary>
        public static void AddSwagger(this IServiceCollection services)
        {

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Version = "v1",
                    Title = "IpInfo.Api",
                    Description = "This is my of test web-api project",
                    TermsOfService = new Uri("https://t.me/esviken"),
                    Contact = new OpenApiContact()
                    {
                        Name = "Telegram",
                        Url = new Uri("https://t.me/esviken")
                    }
                });

                var xmlFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFileName));
            });
        }
    }
}
