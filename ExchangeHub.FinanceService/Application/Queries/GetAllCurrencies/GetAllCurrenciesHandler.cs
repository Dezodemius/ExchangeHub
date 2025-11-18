using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ExchangeHub.FinanceService.Application.Interfaces;
using MediatR;
using Currency = ExchangeHub.FinanceService.Domain.Entities.Currency;
using Entities_Currency = ExchangeHub.FinanceService.Domain.Entities.Currency;

namespace ExchangeHub.FinanceService.Application.Queries.GetAllCurrencies;

public class GetAllCurrenciesHandler(ICurrencyService currencyService)
    : IRequestHandler<GetAllCurrenciesQuery, IList<Entities_Currency>>
{
    public async Task<IList<Currency>> Handle(GetAllCurrenciesQuery request, CancellationToken cancellationToken)
    {
        return await currencyService.GetAllCurrencies(cancellationToken);
    }
}