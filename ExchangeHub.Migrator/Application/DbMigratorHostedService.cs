using System;
using System.Threading;
using System.Threading.Tasks;
using ExchangeHub.Migrator.Infrastrucutre;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ExchangeHub.Migrator.Application;

public class DbMigratorHostedService(IServiceProvider serviceProvider) : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<MigratorDbContext>();
        Console.WriteLine("Apply migrations...");
        await dbContext.Database.MigrateAsync(cancellationToken);
        Console.WriteLine("Successfully applied migrations!");
        Environment.Exit(0);
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}