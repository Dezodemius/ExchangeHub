using System;
using System.Threading;
using System.Threading.Tasks;
using ExchangeHub.Shared;
using Microsoft.EntityFrameworkCore;

namespace ExchangeHub.UserService;

public class UserService : IUserService
{
    private readonly UserServiceDbContext _db;

    private readonly IPasswordHelper _passwordHelper;

    public UserService(UserServiceDbContext db, IPasswordHelper passwordHelper)
    {
        _db = db;
        _passwordHelper = passwordHelper;
    }

    public async Task<User> RegisterAsync(string name, string password, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentNullException(nameof(name));
        if (string.IsNullOrEmpty(password))
            throw new ArgumentNullException(nameof(password));
        if (await _db.Users.AnyAsync(u => u.Name == name, cancellationToken: cancellationToken))
            throw new InvalidOperationException("User already exists");

        var user = new User
        {
            Name = name,
            Password = _passwordHelper.HashPassword(password)
        };

        _db.Users.Add(user);
        await _db.SaveChangesAsync(cancellationToken);
        return user;
    }

    public async Task<User?> AuthenticateAsync(string name, string password, CancellationToken cancellationToken)
    {
        var user = await _db.Users.FirstOrDefaultAsync(u => u.Name == name, cancellationToken: cancellationToken);
        if (user == null) 
            return null;

        return _passwordHelper.VerifyPassword(password, user.Password) ? user : null;
    }
}