using MediatR;
using User = ExchangeHub.UserService.Models.User;

namespace ExchangeHub.UserService.Queries.RegisterUser;

public record RegisterUserQuery(string Username, string Password) : IRequest<User>;
