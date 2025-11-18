using System;

namespace ExchangeHub.Migrator;

public class UserCurrency
{
    public int UserId { get; set; }
    
    public int CurrencyId { get; set; }

    public DateTime AddedAt { get; set; } = DateTime.UtcNow;
}