using System.Collections.Generic;

namespace ExchangeHub.Shared;

public class Currency
{
    public long Id { get; set; }
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
    public decimal Rate { get; set; }
}