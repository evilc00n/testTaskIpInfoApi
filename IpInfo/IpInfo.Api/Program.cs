using IpInfo.Api;
using IpInfo.Application.DependencyInjection;
using IpInfo.Dal.DependencyInjection;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
//Подключение метода-расширения из класса Startup для настройки swagger
builder.Services.AddSwagger();
//Использование Serilog в качестве логгера. Его конфигурация в appsettings
builder.Host.UseSerilog(
    (context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));


//Подключение метода-расширения из класса
//Dependency injection для Infrastructure слоя для его настройки
builder.Services.AddDataAccessLayer(builder.Configuration);

//Аналогично для класса DI из слоя Core
builder.Services.AddApplications();




var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    //Задание uri для json файла swagger и его название. 
    app.UseSwaggerUI(c =>
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "IpInfo Swagger v1.0"));
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
