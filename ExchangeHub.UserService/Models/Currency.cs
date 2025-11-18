using System.Collections.Generic;

namespace ExchangeHub.UserService.Models;

public class Currency
{
    public int Id { get; set; }
    
    public List<UserCurrency> UserCurrencies { get; set; } = new();
}