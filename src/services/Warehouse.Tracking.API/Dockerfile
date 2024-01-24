#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["./src/services/Warehouse.Tracking.API/Warehouse.Tracking.API.csproj", "services/Warehouse.Tracking.API/"]
COPY ["./src/services/Warehouse.Tracking.Application/Warehouse.Tracking.Application.csproj", "services/Warehouse.Tracking.Application/"]
COPY ["./src/services/Warehouse.Tracking.Domain/Warehouse.Tracking.Domain.csproj", "services/Warehouse.Tracking.Domain/"]
COPY ["./src/services/Warehouse.Tracking.Infrastructure/Warehouse.Tracking.Infrastructure.csproj", "services/Warehouse.Tracking.Infrastructure/"]
COPY ["./src/services/Warehouse.Tracking.Shared/Warehouse.Tracking.Shared.csproj", "services/Warehouse.Tracking.Shared/"]

RUN dotnet restore "services/Warehouse.Tracking.API/Warehouse.Tracking.API.csproj"
COPY . .
WORKDIR "src/services/Warehouse.Tracking.API"
RUN dotnet build "Warehouse.Tracking.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Warehouse.Tracking.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Warehouse.Tracking.API.dll"]