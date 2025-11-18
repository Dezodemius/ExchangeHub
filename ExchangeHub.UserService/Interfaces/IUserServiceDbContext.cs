using System.Threading;
using System.Threading.Tasks;
using ExchangeHub.UserService.Models;
using Microsoft.EntityFrameworkCore;
using User = ExchangeHub.UserService.Models.User;

namespace ExchangeHub.UserService;

public interface IUserServiceDbContext
{
    DbSet<User> Users { get; }
    
    DbSet<UserCurrency> UserCurrencies { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}