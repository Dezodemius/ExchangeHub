FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ExchangeHub.CurrencyWorker/ExchangeHub.CurrencyWorker.csproj ExchangeHub.CurrencyWorker/
COPY ExchangeHub.Shared/ExchangeHub.Shared.csproj ExchangeHub.Shared/

RUN dotnet restore ExchangeHub.CurrencyWorker.csproj

COPY ExchangeHub.Shared/ ExchangeHub.Shared/
COPY ExchangeHub.CurrencyWorker/ ExchangeHub.CurrencyWorker/

RUN dotnet publish ExchangeHub.CurrencyWorker.csproj -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "ExchangeHub.CurrencyWorker.dll"]
