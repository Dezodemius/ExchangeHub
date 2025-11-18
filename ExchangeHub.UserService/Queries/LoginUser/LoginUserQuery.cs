using MediatR;

namespace ExchangeHub.UserService.Queries.LoginUser;

public record LoginUserQuery(string Username, string Password) : IRequest<LoginUserResult>;