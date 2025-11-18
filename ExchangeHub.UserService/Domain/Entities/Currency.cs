using System.Collections.Generic;

namespace ExchangeHub.UserService.Domain.Entities;

public class Currency
{
    public int Id { get; set; }
    
    public List<UserCurrency> UserCurrencies { get; set; } = new();
}