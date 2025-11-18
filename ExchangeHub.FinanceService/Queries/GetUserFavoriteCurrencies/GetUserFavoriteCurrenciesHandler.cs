using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ExchangeHub.Shared;
using MediatR;

namespace ExchangeHub.FinanceService.Queries.GetUserFavoriteCurrencies;

public class GetUserFavoriteCurrenciesHandler : IRequestHandler<GetUserFavoriteCurrenciesQuery, IList<UserCurrency>>
{
    private readonly ICurrencyService _currencyService;

    public GetUserFavoriteCurrenciesHandler(ICurrencyService currencyService)
    {
        _currencyService =  currencyService;
    }
    
    public async Task<IList<UserCurrency>> Handle(GetUserFavoriteCurrenciesQuery request, CancellationToken cancellationToken)
    {
        return await _currencyService.GetUserFavoriteCurrencies(request.UserId, cancellationToken);
    }
}