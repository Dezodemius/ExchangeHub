using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ExchangeHub.CurrencyWorker;

public class CurrencyUpdaterService : BackgroundService
{
    private readonly ILogger<CurrencyUpdaterService> _logger;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly HttpClient _httpClient = new();
    private readonly string _url;

    public CurrencyUpdaterService(ILogger<CurrencyUpdaterService> logger, IServiceScopeFactory scopeFactory, IOptions<CbrApiOptions> options)
    {
        _logger = logger;
        _scopeFactory = scopeFactory;
        _url = options.Value.DailyRatesUrl;
    }

    protected override async Task ExecuteAsync(CancellationToken ct)
    {
        _logger.LogInformation("Currency updater started.");

        while (!ct.IsCancellationRequested)
        {
            try
            {
                await UpdateCurrenciesAsync(ct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating currencies");
            }

            await Task.Delay(TimeSpan.FromSeconds(60), ct);
        }
    }

    private async Task UpdateCurrenciesAsync(CancellationToken ct)
    {
        _logger.LogInformation("Fetching CBR XML...");

        using var response = await _httpClient.GetAsync(_url, ct);
        response.EnsureSuccessStatusCode();
        var bytes = await response.Content.ReadAsByteArrayAsync(ct);
        var xmlText = Encoding.GetEncoding("windows-1251").GetString(bytes);

        var doc = XDocument.Parse(xmlText);

        var valutes = doc.Descendants("Valute")
            .Select(x => new Currency
            {
                Code = x.Element("CharCode")?.Value ?? "",
                Name = x.Element("Name")?.Value ?? "",
                Rate = decimal.Parse(x.Element("Value")?.Value?.Replace(',', '.') ?? "0", System.Globalization.CultureInfo.InvariantCulture)
            })
            .ToList();

        using var scope = _scopeFactory.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<CurrencyDbContext>();

        foreach (var val in valutes)
        {
            var existing = await db.Currencies.FirstOrDefaultAsync(c => c.Code == val.Code, ct);
            if (existing is null)
                db.Currencies.Add(val);
            else
            {
                existing.Name = val.Name;
                existing.Rate = val.Rate;
            }
        }

        await db.SaveChangesAsync(ct);
        _logger.LogInformation("Currencies updated: {Count}", valutes.Count);
    }
}