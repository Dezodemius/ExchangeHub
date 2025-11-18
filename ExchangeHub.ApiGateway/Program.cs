using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace ExchangeHub.ApiGateway;

class Program
{
    static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddReverseProxy().LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));
        builder.WebHost.ConfigureKestrel((context, options) =>
        {
            var port = context.Configuration["PORT"];
            if (!string.IsNullOrEmpty(port))
            {
                options.ListenAnyIP(int.Parse(port));
            }
        });

        builder.Services.AddControllers();
        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(p =>
            {
                p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
            });
        });
        builder.WebHost.UseKestrel(); 

        var app = builder.Build();

        app.UseCors();
        app.MapReverseProxy();
        app.MapControllers();

        app.Run();
    }
}