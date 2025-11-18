using System.Collections.Generic;

namespace ExchangeHub.UserService.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Password { get; set; } = null!;

    public List<UserCurrency> FavoriteCurrencies { get; set; } = new();
}