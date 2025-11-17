using System;
using Microsoft.EntityFrameworkCore;

namespace ExchangeHub.UserService.Tests;

public class DbContextCreator
{
    public static UserServiceDbContext Create()
    {
        var options = new DbContextOptionsBuilder<UserServiceDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        
        return new UserServiceDbContext(options);
    }
}