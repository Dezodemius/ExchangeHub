using System.Collections.Generic;

namespace ExchangeHub.FinanceService.Domain.Entities;

public class Currency
{
    public int Id { get; set; }
    
    public string Code { get; set; } = null!;
    
    public string Name { get; set; } = null!;
    
    public decimal Rate { get; set; }

    public List<UserCurrency> UserCurrencies { get; set; } = new();
}