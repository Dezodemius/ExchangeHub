using System.Text;
using ExchangeHub.FinanceService.Queries.GetUserFavoriteCurrencies;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace ExchangeHub.FinanceService;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.WebHost.ConfigureKestrel((context, options) =>
        {
            var port = context.Configuration["PORT"];
            if (!string.IsNullOrEmpty(port))
            {
                options.ListenAnyIP(int.Parse(port));
            }
        });
        builder.Services.AddDbContext<FinanceServiceDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddScoped<IFinanceServiceDbContext>(provider => 
            provider.GetRequiredService<FinanceServiceDbContext>());
        builder.Services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(GetUserFavoriteCurrenciesQuery).Assembly);
        });
        builder.Services.AddControllers();
        var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!);
        builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });
        builder.WebHost.UseKestrel(); 

        var app = builder.Build();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}