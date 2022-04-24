FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Migrations/Migrations.csproj", "Migrations/"]
RUN dotnet restore "Migrations/Migrations.csproj"
COPY . .
WORKDIR "/src/Migrations"
RUN dotnet build "Migrations.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Migrations.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Migrations.dll"]
