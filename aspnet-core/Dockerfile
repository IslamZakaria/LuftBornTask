# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /app

# Install Node.js, NPM and Yarn (required for abp install-libs)
RUN apt-get update \
    && apt-get install -y nodejs npm curl \
    && npm install -g yarn \
    && rm -rf /var/lib/apt/lists/*

# Copy solution and project files
COPY . .

# Restore and build
RUN dotnet restore "src/LuftBornTask.HttpApi.Host/LuftBornTask.HttpApi.Host.csproj"
RUN dotnet build "src/LuftBornTask.HttpApi.Host/LuftBornTask.HttpApi.Host.csproj" -c Release -o /app/build

# Install ABP client-side libraries
RUN dotnet tool install -g Volo.Abp.Cli
ENV PATH="${PATH}:/root/.dotnet/tools"

# Configure Yarn/NPM timeouts and retry abp install-libs to mitigate transient DNS/network failures
RUN yarn config set network-timeout 600000 || true \
        && npm config set registry https://registry.npmjs.org/ || true \
        && for i in 1 2 3 4 5; do \
                 echo "abp install-libs attempt $i"; \
                 abp install-libs && break || (echo "attempt $i failed"; sleep 5); \
             done \
        || (echo "abp install-libs failed after retries" && exit 1)

# Show installed libs for debugging (will be visible in build logs)
RUN if [ -d "src/LuftBornTask.HttpApi.Host/wwwroot/libs" ]; then echo "Libs present:" && ls -la src/LuftBornTask.HttpApi.Host/wwwroot/libs || true; else echo "No libs folder found"; fi

# Stage 2: Publish
FROM build AS publish
RUN dotnet publish "src/LuftBornTask.HttpApi.Host/LuftBornTask.HttpApi.Host.csproj" -c Release -o /app/publish

# Stage 3: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

# Install curl for healthcheck
RUN apt-get update && apt-get install -y curl && rm -rf /var/lib/apt/lists/*

COPY --from=publish /app/publish .

# Expose port (must match ASPNETCORE_URLS)
EXPOSE 5000

# Set environment variables
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS=http://+:5000

HEALTHCHECK --interval=30s --timeout=10s --start-period=10s --retries=3 \
    CMD curl -f http://localhost:5000/health || exit 1

ENTRYPOINT ["dotnet", "LuftBornTask.HttpApi.Host.dll"]
