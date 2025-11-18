using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ExchangeHub.Shared.DTO;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static System.Linq.Queryable;

namespace ExchangeHub.FinanceService.Queries.GetAllCurrencies;

public class GetAllCurrenciesHandler : IRequestHandler<GetAllCurrenciesQuery, IList<CurrencyDto>>
{
    private readonly IFinanceServiceDbContext _db;

    public GetAllCurrenciesHandler(IFinanceServiceDbContext db)
    {
        _db = db;
    }
    
    public async Task<IList<CurrencyDto>> Handle(GetAllCurrenciesQuery request, CancellationToken cancellationToken)
    {
        var currencies = await _db.Currencies
            .Select(c => new CurrencyDto(
                c.Id,
                c.Name,
                c.Rate))
            .ToListAsync(cancellationToken);

        return currencies;
    }
}