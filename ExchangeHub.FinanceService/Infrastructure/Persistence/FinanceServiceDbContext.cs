using ExchangeHub.FinanceService.Application.Interfaces;
using ExchangeHub.FinanceService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Currency = ExchangeHub.FinanceService.Domain.Entities.Currency;

namespace ExchangeHub.FinanceService.Infrastructure.Persistence;

public class FinanceServiceDbContext(DbContextOptions<FinanceServiceDbContext> options)
    : DbContext(options), IFinanceServiceDbContext
{
    public DbSet<Currency> Currencies => Set<Currency>();
    
    public DbSet<UserCurrency> UserCurrencies => Set<UserCurrency>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserCurrency>()
            .HasKey(uc => new { uc.UserId, uc.CurrencyId });

        base.OnModelCreating(modelBuilder);
    }
}