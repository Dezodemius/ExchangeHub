using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ExchangeHub.FinanceService.Application.DTO;
using ExchangeHub.FinanceService.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using static System.Linq.Queryable;
using Currency = ExchangeHub.FinanceService.Domain.Entities.Currency;

namespace ExchangeHub.FinanceService.Application.Services;

public class CurrencyService(IFinanceServiceDbContext db) : ICurrencyService
{
    public async Task<IList<Currency>> GetAllCurrencies(CancellationToken ct)
    {
        return await db.Currencies.ToListAsync(ct);
    }

    public async Task<IList<FavoriteCurrencyDto>> GetUserFavoriteCurrencies(int id, CancellationToken ct)
    {
        return await (
            from uc in db.UserCurrencies
            join c in db.Currencies on uc.CurrencyId equals c.Id
            where uc.UserId == id
            select new FavoriteCurrencyDto(
                c.Id,
                c.Code,
                c.Name,
                c.Rate
            )
        ).ToListAsync(ct);
    }
}