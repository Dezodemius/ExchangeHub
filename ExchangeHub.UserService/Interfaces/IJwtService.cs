using User = ExchangeHub.UserService.Models.User;

namespace ExchangeHub.UserService;

public interface IJwtService
{
    string GenerateJwtToken(User user);
}