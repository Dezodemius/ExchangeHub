using Microsoft.EntityFrameworkCore;

namespace ExchangeHub.CurrencyWorker;

public class CurrencyDbContext : DbContext
{
    public DbSet<Currency> Currencies => Set<Currency>();

    public CurrencyDbContext(DbContextOptions<CurrencyDbContext> options)
        : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Currency>()
            .HasKey(c => c.Id);

        base.OnModelCreating(modelBuilder);
    }
}