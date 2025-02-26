using Blog.Application;
using Blog.Infrastructure;
using Blog.Worker;
using Microsoft.AspNetCore.Http;

var builder = Host.CreateApplicationBuilder(args);

// Configuraci�n
builder.Configuration.SetBasePath(Directory.GetCurrentDirectory());
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

builder.Services.AddHostedService<Worker>();

builder.Services.AddSingleton<IHttpContextAccessor>(new HttpContextAccessor());


// Agregar servicios de infraestructura y aplicaci�n
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices();

var host = builder.Build();

host.Run();
