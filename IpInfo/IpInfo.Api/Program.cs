using IpInfo.Api;
using IpInfo.Application.DependencyInjection;
using IpInfo.Dal.DependencyInjection;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
//����������� ������-���������� �� ������ Startup ��� ��������� swagger
builder.Services.AddSwagger();
//������������� Serilog � �������� �������. ��� ������������ � appsettings
builder.Host.UseSerilog(
    (context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));


//����������� ������-���������� �� ������
//Dependency injection ��� Infrastructure ���� ��� ��� ���������
builder.Services.AddDataAccessLayer(builder.Configuration);

//���������� ��� ������ DI �� ���� Core
builder.Services.AddApplications();




var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    //������� uri ��� json ����� swagger � ��� ��������. 
    app.UseSwaggerUI(c =>
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "IpInfo Swagger v1.0"));
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
