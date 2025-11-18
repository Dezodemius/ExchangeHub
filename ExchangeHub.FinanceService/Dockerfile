FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

EXPOSE 5002

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ./ExchangeHub.FinanceService/ExchangeHub.FinanceService.csproj ./
RUN dotnet restore ExchangeHub.FinanceService.csproj

COPY ./ExchangeHub.FinanceService .
COPY ./ExchangeHub.Shared/ ../ExchangeHub.Shared/

RUN dotnet publish ExchangeHub.FinanceService.csproj -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "ExchangeHub.FinanceService.dll"]