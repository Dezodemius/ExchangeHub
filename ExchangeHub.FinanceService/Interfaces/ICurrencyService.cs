using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ExchangeHub.FinanceService.DTO;
using Currency = ExchangeHub.FinanceService.Models.Currency;

namespace ExchangeHub.FinanceService;

public interface ICurrencyService
{
    Task<IList<Currency>> GetAllCurrencies(CancellationToken cancellationToken );
    
    Task<IList<FavoriteCurrencyDto>> GetUserFavoriteCurrencies(int id, CancellationToken cancellationToken);
}