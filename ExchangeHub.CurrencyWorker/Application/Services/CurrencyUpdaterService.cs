using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using ExchangeHub.CurrencyWorker.Domain.Entities;
using ExchangeHub.CurrencyWorker.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ExchangeHub.CurrencyWorker.Application.Services;

public class CurrencyUpdaterService(
    ILogger<CurrencyUpdaterService> logger,
    IServiceScopeFactory scopeFactory,
    IOptions<CbrApiOptions> options)
    : BackgroundService
{
    private readonly HttpClient _httpClient = new();

    private readonly string _url = options.Value.DailyRatesUrl;

    protected override async Task ExecuteAsync(CancellationToken ct)
    {
        logger.LogInformation("Currency updater started.");

        while (!ct.IsCancellationRequested)
        {
            try
            {
                await UpdateCurrenciesAsync(ct);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error while updating currencies");
            }

            await Task.Delay(TimeSpan.FromSeconds(60), ct);
        }
    }

    private async Task UpdateCurrenciesAsync(CancellationToken ct)
    {
        logger.LogInformation("Fetching CBR XML...");

        using var response = await _httpClient.GetAsync(_url, ct);
        response.EnsureSuccessStatusCode();
        var bytes = await response.Content.ReadAsByteArrayAsync(ct);
        var xmlText = Encoding.GetEncoding("windows-1251").GetString(bytes);

        var doc = XDocument.Parse(xmlText);

        var currencies = doc.Descendants("Valute")
            .Select(x => new Currency
            {
                Code = x.Element("CharCode")?.Value ?? "",
                Name = x.Element("Name")?.Value ?? "",
                Rate = decimal.Parse(x.Element("Value")?.Value?.Replace(',', '.') ?? "0", System.Globalization.CultureInfo.InvariantCulture)
            })
            .ToList();

        using var scope = scopeFactory.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<CurrencyDbContext>();

        foreach (var currency in currencies)
        {
            var existing = await db.Currencies.FirstOrDefaultAsync(c => c.Code == currency.Code, ct);
            if (existing is null)
                db.Currencies.Add(currency);
            else
            {
                existing.Name = currency.Name;
                existing.Rate = currency.Rate;
            }
        }

        await db.SaveChangesAsync(ct);
        logger.LogInformation("Currencies updated: {Count}", currencies.Count);
    }
}