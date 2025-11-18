using MediatR;
using Entities_User = ExchangeHub.UserService.Domain.Entities.User;

namespace ExchangeHub.UserService.Application.Queries.RegisterUser;

public record RegisterUserQuery(string Username, string Password) : IRequest<Entities_User>;
