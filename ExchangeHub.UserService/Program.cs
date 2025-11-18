using ExchangeHub.UserService.Application.Helpers;
using ExchangeHub.UserService.Application.Interfaces;
using ExchangeHub.UserService.Application.Options;
using ExchangeHub.UserService.Application.Queries.AddFavoriteCurrency;
using ExchangeHub.UserService.Application.Queries.LoginUser;
using ExchangeHub.UserService.Application.Queries.RegisterUser;
using ExchangeHub.UserService.Application.Queries.RemoveFavoriteCurrency;
using ExchangeHub.UserService.Application.Services;
using ExchangeHub.UserService.Infrasturcture;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace ExchangeHub.UserService;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<UserServiceDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", policy =>
            {
                policy.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });
        builder.WebHost.ConfigureKestrel((context, options) =>
        {
            options.Configure(context.Configuration.GetSection("Kestrel"));
        });
        builder.Services.AddControllers();
        
        builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(JwtOptions.SectionName));

        builder.Services.AddScoped<IUserServiceDbContext>(provider => 
            provider.GetRequiredService<UserServiceDbContext>());
        builder.Services.AddScoped<IAuthService, AuthService>();
        builder.Services.AddScoped<IFavoriteCurrencyService, FavoriteCurrencyService>();
        builder.Services.AddScoped<IPasswordHelper, PasswordHelper>();
        builder.Services.AddSingleton<IConfigureOptions<JwtBearerOptions>, ConfigureJwtBearerOptions>();
        builder.Services.AddScoped<IJwtService, JwtService>();
        
        builder.Services
            .AddAuthentication()
            .AddJwtBearer();
        builder.Services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(RegisterUserQuery).Assembly);
            cfg.RegisterServicesFromAssembly(typeof(LoginUserQuery).Assembly);
            cfg.RegisterServicesFromAssembly(typeof(AddFavoriteCurrencyQuery).Assembly);
            cfg.RegisterServicesFromAssembly(typeof(RemoveFavoriteCurrencyQuery).Assembly);
        });
        
        builder.WebHost.UseKestrel(); 
        
        var app = builder.Build();

        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
        app.UseCors("AllowAll");

        app.Run();
    }
}