FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ./ExchangeHub.Migrator/ExchangeHub.Migrator.csproj ./
RUN dotnet restore ExchangeHub.Migrator.csproj

COPY ./ExchangeHub.Migrator .
COPY ./ExchangeHub.Shared/ ../ExchangeHub.Shared/

RUN dotnet publish ExchangeHub.Migrator.csproj -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "ExchangeHub.Migrator.dll"]
