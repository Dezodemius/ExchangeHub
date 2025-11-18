using System.Threading;
using System.Threading.Tasks;
using ExchangeHub.Shared;

namespace ExchangeHub.UserService;

public interface IUserService
{
    Task<User> RegisterAsync(string name, string password, CancellationToken cancellationToken);

    Task<User?> AuthenticateAsync(string name, string password, CancellationToken cancellationToken);
}