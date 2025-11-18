using MediatR;

namespace ExchangeHub.UserService.Application.Queries.RemoveFavoriteCurrency;

public record RemoveFavoriteCurrencyQuery(int UserId, int CurrencyId) : IRequest;
