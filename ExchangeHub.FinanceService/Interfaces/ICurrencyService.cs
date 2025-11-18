using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ExchangeHub.Shared;

namespace ExchangeHub.FinanceService;

public interface ICurrencyService
{
    Task<IList<Currency>> GetAllCurrencies(CancellationToken cancellationToken );
    
    Task<IList<UserCurrency>> GetUserFavoriteCurrencies(long id, CancellationToken cancellationToken);
}