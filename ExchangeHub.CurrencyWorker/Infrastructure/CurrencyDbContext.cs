using ExchangeHub.CurrencyWorker.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExchangeHub.CurrencyWorker.Infrastructure;

public class CurrencyDbContext(DbContextOptions<CurrencyDbContext> options) 
    : DbContext(options)
{
    public DbSet<Currency> Currencies => Set<Currency>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Currency>()
            .HasKey(c => c.Id);

        base.OnModelCreating(modelBuilder);
    }
}