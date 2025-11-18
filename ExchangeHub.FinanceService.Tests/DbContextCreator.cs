using System;
using ExchangeHub.FinanceService.Application.Interfaces;
using ExchangeHub.FinanceService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ExchangeHub.FinanceService.Tests;

public class DbContextCreator
{
    public static IFinanceServiceDbContext Create()
    {
        var options = new DbContextOptionsBuilder<FinanceServiceDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        
        return new FinanceServiceDbContext(options);
    }
}