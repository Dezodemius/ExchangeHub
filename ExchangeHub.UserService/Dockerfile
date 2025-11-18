FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ./ExchangeHub.UserService/ExchangeHub.UserService.csproj ./
RUN dotnet restore ExchangeHub.UserService.csproj

COPY ./ExchangeHub.UserService .
COPY ./ExchangeHub.Shared/ ../ExchangeHub.Shared/

RUN dotnet publish ExchangeHub.UserService.csproj -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "ExchangeHub.UserService.dll"]