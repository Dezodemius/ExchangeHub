FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ExchangeHub.UserService/ExchangeHub.UserService.csproj ExchangeHub.UserService/
COPY ExchangeHub.Shared/ExchangeHub.Shared.csproj ExchangeHub.Shared/

RUN dotnet restore ExchangeHub.UserService/ExchangeHub.UserService.csproj

COPY ExchangeHub.Shared/ ExchangeHub.Shared/
COPY ExchangeHub.UserService/ ExchangeHub.UserService/

RUN dotnet publish ExchangeHub.UserService/ExchangeHub.UserService.csproj  -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "ExchangeHub.UserService.dll"]
