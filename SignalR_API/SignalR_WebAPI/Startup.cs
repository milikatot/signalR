using Microsoft.OpenApi.Models;
using SignalR_WebAPI.Hub;

namespace SignalR_WebAPI;

public class Startup
{
    public IConfiguration Configuration { get; }
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSignalR(c =>
        {
            c.EnableDetailedErrors = true;
            c.ClientTimeoutInterval = TimeSpan.FromSeconds(30);
            c.KeepAliveInterval = TimeSpan.FromSeconds(15);
        });

        services.AddControllers();
    }

    public void Configure(WebApplication app)
    {
        IWebHostEnvironment env = app.Environment;

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapHub<ViewHub>("Notify/View")
            .RequireCors(builder =>
            {
                builder
                .WithOrigins("http://localhost:4200")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
            });
        });


    }

}
