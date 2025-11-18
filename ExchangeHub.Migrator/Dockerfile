FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["./ExchangeHub.Migrator/ExchangeHub.Migrator.csproj", "ExchangeHub.Migrator/"]

RUN dotnet restore "ExchangeHub.Migrator/ExchangeHub.Migrator.csproj"

COPY . .

WORKDIR "/src/ExchangeHub.Migrator"
RUN dotnet build "ExchangeHub.Migrator.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ExchangeHub.Migrator.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "ExchangeHub.Migrator.dll"]