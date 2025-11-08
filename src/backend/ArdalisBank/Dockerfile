FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /backend

COPY *.sln .
COPY Directory.Build.props ./
COPY Directory.Packages.props ./

COPY src/ src/

WORKDIR /backend/src/ArdalisBank.Web

RUN dotnet restore ArdalisBank.Web.csproj

RUN dotnet publish ArdalisBank.Web.csproj -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:90
EXPOSE 90

COPY --from=build /app/publish ./

ENTRYPOINT ["dotnet", "ArdalisBank.Web.dll"]
