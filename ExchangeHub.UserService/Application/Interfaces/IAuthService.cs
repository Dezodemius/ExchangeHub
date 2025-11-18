using System.Threading;
using System.Threading.Tasks;
using User = ExchangeHub.UserService.Domain.Entities.User;

namespace ExchangeHub.UserService.Application.Interfaces;

public interface IAuthService
{
    Task<User> RegisterAsync(string name, string password, CancellationToken cancellationToken);

    Task<User?> AuthenticateAsync(string name, string password, CancellationToken cancellationToken);
}