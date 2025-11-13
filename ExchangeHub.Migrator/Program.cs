using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ExchangeHub.Migrator;

class Program
{
    private readonly MigratorDbContext _dbContext;

    static async Task Main(string[] args)
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