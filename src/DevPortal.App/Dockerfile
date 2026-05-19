# Stage 1 — Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

COPY src/DevPortal.HealthCheck/ ./DevPortal.HealthCheck/
COPY src/DevPortal.App/ ./DevPortal.App/

RUN dotnet restore ./DevPortal.App/DevPortal.App.csproj

RUN dotnet publish ./DevPortal.App/DevPortal.App.csproj \
    -c Release \
    -o /app/publish \
    --no-restore

# Stage 2 — Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0.21-noble-chiseled AS runtime

USER app

WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080

ENTRYPOINT ["dotnet", "DevPortal.App.dll"]