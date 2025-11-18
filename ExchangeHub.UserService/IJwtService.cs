using ExchangeHub.Shared;

namespace ExchangeHub.UserService;

public interface IJwtService
{
    string GenerateJwtToken(User user);
}