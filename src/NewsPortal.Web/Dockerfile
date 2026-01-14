FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj files and restore
COPY ["NewsPortal.Web/NewsPortal.Web.csproj", "NewsPortal.Web/"]
COPY ["NewsPortal.Application/NewsPortal.Application.csproj", "NewsPortal.Application/"]
COPY ["NewsPortal.Infrastructure/NewsPortal.Infrastructure.csproj", "NewsPortal.Infrastructure/"]
COPY ["NewsPortal.Core/NewsPortal.Core.csproj", "NewsPortal.Core/"]
RUN dotnet restore "NewsPortal.Web/NewsPortal.Web.csproj"

# Copy everything else and build
COPY . .
WORKDIR "/src/NewsPortal.Web"
RUN dotnet build "NewsPortal.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NewsPortal.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NewsPortal.Web.dll"]
