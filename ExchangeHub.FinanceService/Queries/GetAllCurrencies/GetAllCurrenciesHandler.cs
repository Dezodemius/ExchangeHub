using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Currency = ExchangeHub.FinanceService.Models.Currency;

namespace ExchangeHub.FinanceService.Queries.GetAllCurrencies;

public class GetAllCurrenciesHandler : IRequestHandler<GetAllCurrenciesQuery, IList<Currency>>
{
    private readonly ICurrencyService _currencyService;

    public GetAllCurrenciesHandler(ICurrencyService currencyService)
    {
        _currencyService = currencyService;
    }
    
    public async Task<IList<Currency>> Handle(GetAllCurrenciesQuery request, CancellationToken cancellationToken)
    {
        return await _currencyService.GetAllCurrencies(cancellationToken);
    }
}