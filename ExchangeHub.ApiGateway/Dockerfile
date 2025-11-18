FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ExchangeHub.ApiGateway/ExchangeHub.ApiGateway.csproj ExchangeHub.ApiGateway/

RUN dotnet restore ExchangeHub.ApiGateway/ExchangeHub.ApiGateway.csproj

COPY ExchangeHub.ApiGateway/ ExchangeHub.ApiGateway/

RUN dotnet publish ExchangeHub.ApiGateway/ExchangeHub.ApiGateway.csproj -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "ExchangeHub.ApiGateway.dll"]
