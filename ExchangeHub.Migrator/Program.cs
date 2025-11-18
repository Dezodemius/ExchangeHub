using System;
using System.Threading.Tasks;
using ExchangeHub.Migrator.Application;
using ExchangeHub.Migrator.Infrastrucutre;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ExchangeHub.Migrator;

internal class Program
{
    private readonly MigratorDbContext _dbContext;

    private static async Task Main(string[] args)
    {
        Console.WriteLine("Starting database migration...");

        await Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, services) =>
            {
                var connectionString = context.Configuration.GetConnectionString("DefaultConnection");

                services.AddDbContext<MigratorDbContext>(opt =>
                {
                    opt.UseNpgsql(connectionString);
                });

                services.AddHostedService<DbMigratorHostedService>();
            })
            .RunConsoleAsync();
    }
}