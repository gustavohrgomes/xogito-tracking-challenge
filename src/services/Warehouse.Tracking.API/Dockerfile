#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["./src/services/RetailSystem.API/RetailSystem.API.csproj", "src/services/RetailSystem.API/"]

RUN dotnet restore "./src/services/RetailSystem.API/./RetailSystem.API.csproj"
COPY . .
WORKDIR "/src/src/services/RetailSystem.API"
RUN dotnet build "./RetailSystem.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
RUN dotnet publish "./RetailSystem.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RetailSystem.API.dll"]