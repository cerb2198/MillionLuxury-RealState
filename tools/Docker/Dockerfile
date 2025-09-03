FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build-env
WORKDIR /src

COPY src/MillionLuxury.RealEstate.API/MillionLuxury.RealEstate.API.csproj src/MillionLuxury.RealEstate.API/
COPY src/MillionLuxury.RealEstate.Application/MillionLuxury.RealEstate.Application.csproj src/MillionLuxury.RealEstate.Application/
COPY src/MillionLuxury.RealEstate.Domain/MillionLuxury.RealEstate.Domain.csproj src/MillionLuxury.RealEstate.Domain/
COPY src/MillionLuxury.RealEstate.Infrastructure/MillionLuxury.RealEstate.Infrastructure.csproj src/MillionLuxury.RealEstate.Infrastructure/

RUN dotnet restore src/MillionLuxury.RealEstate.API/MillionLuxury.RealEstate.API.csproj

COPY src/ src/

RUN dotnet publish src/MillionLuxury.RealEstate.API/MillionLuxury.RealEstate.API.csproj -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app

COPY --from=build-env /app .

EXPOSE 8080

ENTRYPOINT ["dotnet", "MillionLuxury.RealEstate.API.dll"]