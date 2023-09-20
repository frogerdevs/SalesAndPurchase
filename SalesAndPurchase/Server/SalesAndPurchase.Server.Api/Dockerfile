#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/SalesAndPurchase.Server.Api/SalesAndPurchase.Server.Api.csproj", "src/SalesAndPurchase.Server.Api/"]
COPY ["src/SalesAndPurchase.Server.Application/SalesAndPurchase.Server.Application.csproj", "src/SalesAndPurchase.Server.Application/"]
COPY ["src/SalesAndPurchase.Server.Domain/SalesAndPurchase.Server.Domain.csproj", "src/SalesAndPurchase.Server.Domain/"]
COPY ["src/SalesAndPurchase.Server.Infrastructure/SalesAndPurchase.Server.Infrastructure.csproj", "src/SalesAndPurchase.Server.Infrastructure/"]
RUN dotnet restore "src/SalesAndPurchase.Server.Api/SalesAndPurchase.Server.Api.csproj"
COPY . .
WORKDIR "/src/src/SalesAndPurchase.Server.Api"
RUN dotnet build "SalesAndPurchase.Server.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SalesAndPurchase.Server.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SalesAndPurchase.Server.Api.dll"]