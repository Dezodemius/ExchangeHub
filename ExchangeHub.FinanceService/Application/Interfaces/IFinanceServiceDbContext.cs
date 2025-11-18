using System.Threading;
using System.Threading.Tasks;
using ExchangeHub.FinanceService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Currency = ExchangeHub.FinanceService.Domain.Entities.Currency;

namespace ExchangeHub.FinanceService.Application.Interfaces;

public interface IFinanceServiceDbContext
{
    DbSet<Currency> Currencies { get; }

    DbSet<UserCurrency> UserCurrencies { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}