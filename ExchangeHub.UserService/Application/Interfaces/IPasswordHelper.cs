namespace ExchangeHub.UserService.Application.Interfaces;

public interface IPasswordHelper
{
    string HashPassword(string password);

    bool VerifyPassword(string password, string storedHash);
}