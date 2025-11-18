FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

EXPOSE 5002

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["ExchangeHub.FinanceService/ExchangeHub.FinanceService.csproj", "ExchangeHub.FinanceService/"]

RUN dotnet restore "ExchangeHub.FinanceService/ExchangeHub.FinanceService.csproj"

COPY . .

WORKDIR "/src/ExchangeHub.FinanceService"
RUN dotnet build "ExchangeHub.FinanceService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ExchangeHub.FinanceService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "ExchangeHub.FinanceService.dll"]