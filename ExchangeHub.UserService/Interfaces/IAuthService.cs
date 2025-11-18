using System.Threading;
using System.Threading.Tasks;
using User = ExchangeHub.UserService.Models.User;

namespace ExchangeHub.UserService;

public interface IAuthService
{
    Task<User> RegisterAsync(string name, string password, CancellationToken cancellationToken);

    Task<User?> AuthenticateAsync(string name, string password, CancellationToken cancellationToken);
}