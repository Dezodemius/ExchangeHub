using User = ExchangeHub.UserService.Domain.Entities.User;

namespace ExchangeHub.UserService.Application.Interfaces;

public interface IJwtService
{
    string GenerateJwtToken(User user);
}