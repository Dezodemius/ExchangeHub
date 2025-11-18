using MediatR;

namespace ExchangeHub.UserService.Application.Queries.AddFavoriteCurrency;

public record AddFavoriteCurrencyQuery(int UserId, int CurrencyId) : IRequest;