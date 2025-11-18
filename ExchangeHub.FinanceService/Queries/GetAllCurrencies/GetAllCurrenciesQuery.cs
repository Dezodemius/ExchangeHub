using System.Collections.Generic;
using ExchangeHub.Shared.DTO;
using MediatR;

namespace ExchangeHub.FinanceService.Queries.GetAllCurrencies;

public record GetAllCurrenciesQuery() : IRequest<IList<CurrencyDto>>;