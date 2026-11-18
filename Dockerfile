FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY Logistic.Domain/Logistic.Domain.csproj Logistic.Domain/
COPY Logistic.ApplicationService/Logistic.ApplicationService.csproj Logistic.ApplicationService/
COPY Logistic.Infrastructure/Logistic.Infrastructure.csproj Logistic.Infrastructure/
COPY Logistic.Api/Logistic.Api.csproj Logistic.Api/

RUN dotnet restore Logistic.Api/Logistic.Api.csproj

COPY Logistic.Domain/ Logistic.Domain/
COPY Logistic.ApplicationService/ Logistic.ApplicationService/
COPY Logistic.Infrastructure/ Logistic.Infrastructure/
COPY Logistic.Api/ Logistic.Api/

RUN dotnet publish Logistic.Api/Logistic.Api.csproj -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
RUN apt-get update \
    && apt-get install -y --no-install-recommends curl \
    && rm -rf /var/lib/apt/lists/*
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
HEALTHCHECK --interval=10s --timeout=5s --retries=12 --start-period=30s \
    CMD curl -fsS http://localhost:8080/health/live > /dev/null || exit 1
ENTRYPOINT ["dotnet", "Logistic.Api.dll"]