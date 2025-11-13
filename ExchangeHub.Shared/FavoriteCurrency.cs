using System;

namespace ExchangeHub.Shared;

public class UserCurrency
{
    public long UserId { get; set; }

    public User User { get; set; } = null!;

    public long CurrencyId { get; set; }

    public Currency Currency { get; set; } = null!;

    public DateTime AddedAt { get; set; } = DateTime.UtcNow;
}