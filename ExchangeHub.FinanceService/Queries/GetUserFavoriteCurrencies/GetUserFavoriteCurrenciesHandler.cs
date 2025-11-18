using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ExchangeHub.Shared.DTO;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static System.Linq.Queryable;

namespace ExchangeHub.FinanceService.Queries.GetUserFavoriteCurrencies;

public class GetUserFavoriteCurrenciesHandler : IRequestHandler<GetUserFavoriteCurrenciesQuery, IList<CurrencyDto>>
{
    private readonly IFinanceServiceDbContext _db;

    public GetUserFavoriteCurrenciesHandler(IFinanceServiceDbContext db)
    {
        _db = db;
    }
    
    public async Task<IList<CurrencyDto>> Handle(GetUserFavoriteCurrenciesQuery request, CancellationToken cancellationToken)
    {
        var currencies = await _db.UserCurrencies
            .Where(uc => uc.UserId == request.UserId)
            .Include(uc => uc.Currency)
            .Select(uc => new CurrencyDto(
                uc.Currency.Id,
                uc.Currency.Name,
                uc.Currency.Rate))
            .ToListAsync(cancellationToken);

        return currencies;
    }
}