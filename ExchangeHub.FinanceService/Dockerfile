FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

EXPOSE 5002

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ExchangeHub.FinanceService/ExchangeHub.FinanceService.csproj ExchangeHub.FinanceService/
COPY ExchangeHub.Shared/ExchangeHub.Shared.csproj ExchangeHub.Shared/

RUN dotnet restore ExchangeHub.FinanceService/ExchangeHub.FinanceService.csproj

COPY ExchangeHub.Shared/ ExchangeHub.Shared/
COPY ExchangeHub.FinanceService/ ExchangeHub.FinanceService/

RUN dotnet publish ExchangeHub.FinanceService/ExchangeHub.FinanceService.csproj -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "ExchangeHub.FinanceService.dll"]