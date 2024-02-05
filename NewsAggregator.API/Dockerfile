FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

COPY ["./publish", "."]

ENTRYPOINT ["dotnet", "NewsAggregator.API.dll"]
# ENTRYPOINT ["dotnet", "NewsAggregator.API.dll", "--urls", "http://localhost:7281"]
