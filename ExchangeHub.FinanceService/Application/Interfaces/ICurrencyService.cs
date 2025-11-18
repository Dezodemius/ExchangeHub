using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ExchangeHub.FinanceService.Application.DTO;
using Currency = ExchangeHub.FinanceService.Domain.Entities.Currency;

namespace ExchangeHub.FinanceService.Application.Interfaces;

public interface ICurrencyService
{
    Task<IList<Currency>> GetAllCurrencies(CancellationToken cancellationToken );
    
    Task<IList<FavoriteCurrencyDto>> GetUserFavoriteCurrencies(int id, CancellationToken cancellationToken);
}