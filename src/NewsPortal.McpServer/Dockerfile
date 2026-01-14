FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj files and restore
COPY ["NewsPortal.McpServer/NewsPortal.McpServer.csproj", "NewsPortal.McpServer/"]
COPY ["NewsPortal.Application/NewsPortal.Application.csproj", "NewsPortal.Application/"]
COPY ["NewsPortal.Infrastructure/NewsPortal.Infrastructure.csproj", "NewsPortal.Infrastructure/"]
COPY ["NewsPortal.Core/NewsPortal.Core.csproj", "NewsPortal.Core/"]
RUN dotnet restore "NewsPortal.McpServer/NewsPortal.McpServer.csproj"

# Copy everything else and build
COPY . .
WORKDIR "/src/NewsPortal.McpServer"
RUN dotnet build "NewsPortal.McpServer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NewsPortal.McpServer.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NewsPortal.McpServer.dll"]
