using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ExchangeHub.CurrencyWorker;

public class Program
{
    public static async Task Main(string[] args)
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        
        var builder = Host.CreateApplicationBuilder(args);

        builder.Services.AddDbContext<CurrencyDbContext>(options =>
        {
            var conn = builder.Configuration.GetConnectionString("Postgres");
            options.UseNpgsql(conn);
        });
        builder.Services.Configure<CbrApiOptions>(builder.Configuration.GetSection(CbrApiOptions.SectionName));
        builder.Services.AddHostedService<CurrencyUpdaterService>();

        var host = builder.Build();
        await host.RunAsync();
    }
}