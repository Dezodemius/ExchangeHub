using System.Threading;
using System.Threading.Tasks;
using ExchangeHub.UserService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using User = ExchangeHub.UserService.Domain.Entities.User;

namespace ExchangeHub.UserService.Application.Interfaces;

public interface IUserServiceDbContext
{
    DbSet<User> Users { get; }
    
    DbSet<UserCurrency> UserCurrencies { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}