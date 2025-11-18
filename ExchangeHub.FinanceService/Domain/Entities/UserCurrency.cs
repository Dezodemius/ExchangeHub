using System;

namespace ExchangeHub.FinanceService.Domain.Entities;

public class UserCurrency
{
    public int UserId { get; set; }
    
    public int CurrencyId { get; set; }

    public DateTime AddedAt { get; set; } = DateTime.UtcNow;
}