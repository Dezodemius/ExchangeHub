using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ExchangeHub.Migrator;

public class DbMigratorHostedService : IHostedService
{
    private readonly IServiceProvider _serviceProvider;

    public DbMigratorHostedService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task StartAsync(System.Threading.CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<MigratorDbContext>();
        Console.WriteLine("Применяем миграции к базе...");
        await dbContext.Database.MigrateAsync(cancellationToken);
        Console.WriteLine("Миграции успешно применены!");
        Environment.Exit(0);
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}