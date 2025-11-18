namespace ExchangeHub.CurrencyWorker.Application;

public class CbrApiOptions
{
    public const string SectionName = "CbrApi";

    public string DailyRatesUrl { get; set; } = string.Empty;
}