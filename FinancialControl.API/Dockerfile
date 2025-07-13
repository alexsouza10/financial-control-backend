FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY FinancialControl.sln ./

COPY FinancialControl.API/*.csproj FinancialControl.API/
COPY FinancialControl.Application/*.csproj FinancialControl.Application/
COPY FinancialControl.Domain/*.csproj FinancialControl.Domain/
COPY FinancialControl.Infrastructure/*.csproj FinancialControl.Infrastructure/

RUN dotnet restore FinancialControl.sln

COPY . .

WORKDIR "/src/FinancialControl.API"

RUN dotnet publish "FinancialControl.API.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS="http://+:7000"
EXPOSE 7000

ENTRYPOINT ["dotnet", "FinancialControl.API.dll"]
