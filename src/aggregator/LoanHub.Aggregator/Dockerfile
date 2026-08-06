FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /aggregator

COPY *.sln .
COPY Directory.Build.props ./
COPY Directory.Packages.props ./
COPY src/LoanHub.Aggregator.Web/*.csproj src/LoanHub.Aggregator.Web/
RUN dotnet restore src/LoanHub.Aggregator.Web/LoanHub.Aggregator.Web.csproj

COPY src/ src/

WORKDIR /aggregator/src/LoanHub.Aggregator.Web

RUN dotnet publish LoanHub.Aggregator.Web.csproj -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:100
EXPOSE 100

COPY --from=build /app/publish ./

ENTRYPOINT ["dotnet", "LoanHub.Aggregator.Web.dll"]
