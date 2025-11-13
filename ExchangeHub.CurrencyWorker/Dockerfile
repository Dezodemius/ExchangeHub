FROM mcr.microsoft.com/dotnet/runtime:9.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ExchangeHub.CurrencyWorker/ExchangeHub.CurrencyWorker.csproj", "ExchangeHub.CurrencyWorker/"]
RUN dotnet restore "ExchangeHub.CurrencyWorker/ExchangeHub.CurrencyWorker.csproj"
COPY . .
WORKDIR "/src/ExchangeHub.CurrencyWorker"
RUN dotnet build "./ExchangeHub.CurrencyWorker.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ExchangeHub.CurrencyWorker.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ExchangeHub.CurrencyWorker.dll"]
