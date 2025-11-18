using System;
using System.Threading;
using System.Threading.Tasks;
using ExchangeHub.UserService.Application.Interfaces;
using ExchangeHub.UserService.Infrasturcture;
using Microsoft.EntityFrameworkCore;
using User = ExchangeHub.UserService.Domain.Entities.User;

namespace ExchangeHub.UserService.Application.Services;

public class AuthService(UserServiceDbContext db, IPasswordHelper passwordHelper) : IAuthService
{
    public async Task<User> RegisterAsync(string name, string password, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentNullException(nameof(name));
        if (string.IsNullOrEmpty(password))
            throw new ArgumentNullException(nameof(password));
        if (await db.Users.AnyAsync(u => u.Name == name, cancellationToken: cancellationToken))
            throw new InvalidOperationException("User already exists");

        var user = new User
        {
            Name = name,
            Password = passwordHelper.HashPassword(password)
        };

        db.Users.Add(user);
        await db.SaveChangesAsync(cancellationToken);
        return user;
    }

    public async Task<User?> AuthenticateAsync(string name, string password, CancellationToken cancellationToken)
    {
        var user = await db.Users.FirstOrDefaultAsync(u => u.Name == name, cancellationToken: cancellationToken);
        if (user == null) 
            return null;

        return passwordHelper.VerifyPassword(password, user.Password) ? user : null;
    }
}