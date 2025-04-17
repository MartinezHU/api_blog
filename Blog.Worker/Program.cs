using Blog.Application;
using Blog.Infrastructure;
using Blog.Worker;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting.WindowsServices;

// Configuraci�n del host para ejecutar como servicio de Windows
var builder = Host.CreateApplicationBuilder(new HostApplicationBuilderSettings
{
    Args = args,
    // Esto permite que la aplicaci�n se ejecute como un servicio de Windows
    ContentRootPath = WindowsServiceHelpers.IsWindowsService() ? AppContext.BaseDirectory : null,
});

// Configuraci�n
string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
builder.Configuration.SetBasePath(appDirectory);
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// Registrar el Worker como servicio hospedado
builder.Services.AddHostedService<Worker>();

// Registrar IHttpContextAccessor
builder.Services.AddSingleton<IHttpContextAccessor>(new HttpContextAccessor());


// Agregar servicios de infraestructura y aplicaci�n
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices();


builder.Services.AddWindowsService(options =>
{
    options.ServiceName = "BlogWorkerService";
});

var host = builder.Build();
host.Run();
