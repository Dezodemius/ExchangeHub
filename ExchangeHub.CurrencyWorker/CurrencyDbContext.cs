using ExchangeHub.Shared;
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
        modelBuilder.Entity<Currency>(entity =>
        {
            entity.ToTable("currency");
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Code).IsRequired();
            entity.Property(c => c.Name).IsRequired();
            entity.Property(c => c.Rate).HasColumnType("numeric(18,4)");
        });
    }
}