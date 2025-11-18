FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["ExchangeHub/ExchangeHub.ApiGateway/ExchangeHub.ApiGateway.csproj", "ExchangeHub.ApiGateway/"]

RUN dotnet restore "ExchangeHub.ApiGateway/ExchangeHub.ApiGateway.csproj"

COPY . .

WORKDIR "/src/ExchangeHub.ApiGateway"
RUN dotnet build "ExchangeHub.ApiGateway.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ExchangeHub.ApiGateway.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "ExchangeHub.ApiGateway.dll"]