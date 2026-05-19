# Stage 1 — Build
FROM mcr.microsoft.com/dotnet/sdk:8.0-noble AS build

WORKDIR /src

# Copy the library — matching exact folder structure
# so the relative path in csproj resolves correctly
COPY src/DevPortal.HealthCheck/ ./DevPortal.HealthCheck/
COPY src/DevPortal.App/ ./DevPortal.App/

# Build and publish
RUN dotnet publish ./DevPortal.App/DevPortal.App.csproj \
    -c Release \
    -o /app/publish \
    --no-restore

# Stage 2 — Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0.16-noble-chiseled AS runtime

USER app

WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080

ENTRYPOINT ["dotnet", "DevPortal.App.dll"]