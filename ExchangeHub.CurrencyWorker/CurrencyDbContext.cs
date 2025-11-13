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
}