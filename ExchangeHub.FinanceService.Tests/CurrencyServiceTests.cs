using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ExchangeHub.FinanceService.Application.DTO;
using ExchangeHub.FinanceService.Application.Services;
using ExchangeHub.FinanceService.Domain.Entities;

namespace ExchangeHub.FinanceService.Tests;

public class CurrencyServiceTests
{
    [Test]
    public async Task GetAllCurrencies_ReturnsAllItems()
    {
        var db = DbContextCreator.Create();

        db.Currencies.AddRange(new List<Currency>
        {
            new Currency
            {
                Id = 1,
                Code = "USD",
                Name = "US Dollar",
                Rate = 90m
            },
            new Currency
            {
                Id = 2,
                Code = "EUR",
                Name = "Euro",
                Rate = 100m
            },
        });

        await db.SaveChangesAsync(CancellationToken.None);

        var service = new CurrencyService(db);

        var result = await service.GetAllCurrencies(CancellationToken.None);

        Assert.Multiple(() =>
        {
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result, Has.Some.Matches<Currency>(c => c.Code == "USD"));
            Assert.That(result, Has.Some.Matches<Currency>(c => c.Code == "EUR"));
        });
    }
    
    [Test]
    public async Task GetUserFavoriteCurrencies_ReturnsJoinedDtos()
    {
        var db = DbContextCreator.Create();

        db.Currencies.AddRange(new List<Currency>
        {
            new Currency{
                Id = 1,
                Code = "USD",
                Name = "US Dollar",
                Rate = 90m
                
            },
            new Currency{
                Id = 2,
                Code = "EUR",
                Name = "Euro",
                Rate = 100m}
        });

        db.UserCurrencies.AddRange(new List<UserCurrency>
        {
            new UserCurrency
            {
                UserId = 5,
                CurrencyId = 1
            },
            new UserCurrency
            {
                UserId = 5,
                CurrencyId = 2
            },
            new UserCurrency
            {
                UserId = 99,
                CurrencyId = 1
            },
        });

        await db.SaveChangesAsync(CancellationToken.None);

        var service = new CurrencyService(db);

        var result = await service.GetUserFavoriteCurrencies(5,
            CancellationToken.None);

        Assert.Multiple(() =>
        {
            Assert.That(result,
                Has.Count.EqualTo(2));
            Assert.That(result,
                Has.Some.Matches<FavoriteCurrencyDto>(d => d.Code == "USD"));
            Assert.That(result,
                Has.Some.Matches<FavoriteCurrencyDto>(d => d.Code == "EUR"));

        });
    }
}