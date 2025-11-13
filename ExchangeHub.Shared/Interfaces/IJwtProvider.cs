namespace ExchangeHub.Shared;

public interface IJwtProvider
{
    string GenerateToken(User user);
}
