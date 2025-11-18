using System.Collections.Generic;
using MediatR;
using Entities_Currency = ExchangeHub.FinanceService.Domain.Entities.Currency;

namespace ExchangeHub.FinanceService.Application.Queries.GetAllCurrencies;

public record GetAllCurrenciesQuery : IRequest<IList<Entities_Currency>>;