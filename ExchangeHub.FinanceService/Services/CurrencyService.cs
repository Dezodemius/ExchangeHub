using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ExchangeHub.FinanceService.DTO;
using ExchangeHub.FinanceService.Models;
using Microsoft.EntityFrameworkCore;
using static System.Linq.Queryable;
using Currency = ExchangeHub.FinanceService.Models.Currency;

namespace ExchangeHub.FinanceService;

public class CurrencyService : ICurrencyService
{
    private readonly IFinanceServiceDbContext _db;

    public CurrencyService(IFinanceServiceDbContext db)
    {
        _db = db;
    }

    public async Task<IList<Currency>> GetAllCurrencies(CancellationToken ct)
    {
        return await _db.Currencies.ToListAsync(ct);
    }

    public async Task<IList<FavoriteCurrencyDto>> GetUserFavoriteCurrencies(int id, CancellationToken ct)
    {
        return await (
            from uc in _db.UserCurrencies
            join c in _db.Currencies on uc.CurrencyId equals c.Id
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